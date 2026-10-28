using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace MobileMMORPG.Prototype
{
    [DefaultExecutionOrder(-200)]
    public sealed class PrototypeTouchControls : MonoBehaviour
    {
        private const int SimulatedMouseId = -2;
        private readonly PrototypeTouchState state = new PrototypeTouchState();
        private Rect safeArea;
        private Vector2 screenSize;
        private Texture2D disk;
        private bool enhancedTouchEnabled;
        private bool suspended;
        private bool mouseCaptured;
        private GUIStyle labelStyle;

        public Vector2 MoveAxis => state.MoveAxis;
        public Vector2 LookDegrees => state.LookDelta * (180f / Mathf.Max(safeArea.height, 1f));
        public bool Looking => state.Looking;
        private bool Visible => Application.isMobilePlatform || Application.isEditor || Touchscreen.current != null;

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            enhancedTouchEnabled = true;
            RefreshLayout();
        }

        private void RefreshLayout()
        {
            screenSize = new Vector2(Screen.width, Screen.height);
            safeArea = Screen.safeArea;
            if (safeArea.width <= 0f || safeArea.height <= 0f)
                safeArea = new Rect(0f, 0f, Screen.width, Screen.height);
            state.Configure(safeArea);
            mouseCaptured = false;
        }

        private void Update()
        {
            state.BeginFrame();
            if (!Visible || suspended || !Application.isFocused)
            {
                ResetInput();
                return;
            }
            Rect currentSafe = Screen.safeArea;
            if (screenSize != new Vector2(Screen.width, Screen.height) ||
                (currentSafe.width > 0f && currentSafe.height > 0f && currentSafe != safeArea))
            {
                RefreshLayout();
                return; // 화면 회전 전부터 누르고 있던 손가락을 새 입력으로 받지 않는다.
            }

            bool movePresent = false;
            bool lookPresent = false;
            foreach (Touch touch in Touch.activeTouches)
            {
                if (touch.phase == TouchPhase.Began)
                    state.Begin(touch.touchId, touch.screenPosition);
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    state.End(touch.touchId);
                else
                    state.Move(touch.touchId, touch.screenPosition);
                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                {
                    movePresent |= state.OwnsMove(touch.touchId);
                    lookPresent |= state.OwnsLook(touch.touchId);
                }
            }

            // Mac의 Game 화면에서는 왼쪽 드래그로 한쪽씩 시험한다. 실기기는 터치만 받는다.
            if (Application.isEditor && Touchscreen.current == null)
            {
                Mouse mouse = Mouse.current;
                if (mouse != null)
                {
                    Vector2 position = mouse.position.ReadValue();
                    if (mouse.leftButton.wasPressedThisFrame)
                    {
                        mouseCaptured = true;
                        state.Begin(SimulatedMouseId, position);
                    }
                    else if (mouseCaptured && mouse.leftButton.isPressed)
                        state.Move(SimulatedMouseId, position);
                    if (mouseCaptured && !mouse.leftButton.isPressed)
                    {
                        state.End(SimulatedMouseId);
                        mouseCaptured = false;
                    }
                    if (mouseCaptured && mouse.leftButton.isPressed)
                    {
                        movePresent |= state.OwnsMove(SimulatedMouseId);
                        lookPresent |= state.OwnsLook(SimulatedMouseId);
                    }
                }
            }
            // 종료 이벤트 없이 장치·손가락이 사라져도 입력이 눌린 채 남지 않게 한다.
            state.ReleaseMissing(movePresent, lookPresent);
        }

        private void ResetInput()
        {
            state.Reset();
            mouseCaptured = false;
        }

        private void OnApplicationFocus(bool focused)
        {
            if (!focused) ResetInput();
        }

        private void OnApplicationPause(bool paused)
        {
            suspended = paused;
            ResetInput();
        }

        private void OnDisable()
        {
            ResetInput();
            if (enhancedTouchEnabled)
            {
                EnhancedTouchSupport.Disable();
                enhancedTouchEnabled = false;
            }
            if (disk != null) Destroy(disk);
            disk = null;
        }

        private void OnGUI()
        {
            if (!Visible || Event.current.type != EventType.Repaint || safeArea.height <= 0f) return;
            if (disk == null) CreateDisk();
            if (labelStyle == null)
                labelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            labelStyle.fontSize = Mathf.RoundToInt(Mathf.Clamp(safeArea.height * 0.035f, 14f, 48f));
            Color previousColor = GUI.color;
            float radius = state.Radius;
            DrawDisk(state.MoveCenter, radius, new Color(0.05f, 0.12f, 0.2f, 0.55f));
            DrawDisk(state.MoveCenter + state.MoveAxis * radius * 0.65f, radius * 0.35f,
                new Color(0.35f, 0.8f, 1f, 0.9f));
            GUI.color = Color.white;
            GUI.Label(ToGuiRect(new Rect(state.MoveCenter.x - radius, safeArea.yMin,
                radius * 2f, radius * 0.38f)), "MOVE", labelStyle);
            GUI.Label(ToGuiRect(new Rect(safeArea.center.x, safeArea.yMin + radius * 0.4f,
                safeArea.width * 0.5f, radius * 0.7f)), "LOOK", labelStyle);
            GUI.color = previousColor;
        }

        private void DrawDisk(Vector2 center, float radius, Color color)
        {
            GUI.color = color;
            GUI.DrawTexture(ToGuiRect(new Rect(center.x - radius, center.y - radius,
                radius * 2f, radius * 2f)), disk);
        }

        private static Rect ToGuiRect(Rect rect)
            => new Rect(rect.x, Screen.height - rect.yMax, rect.width, rect.height);

        private void CreateDisk()
        {
            const int size = 64;
            disk = new Texture2D(size, size, TextureFormat.RGBA32, false);
            disk.name = "Prototype touch disk";
            disk.wrapMode = TextureWrapMode.Clamp;
            Color[] pixels = new Color[size * size];
            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                {
                    float d = Vector2.Distance(new Vector2(x + 0.5f, y + 0.5f), Vector2.one * size * 0.5f);
                    pixels[y * size + x] = new Color(1f, 1f, 1f, Mathf.Clamp01(size * 0.5f - d));
                }
            disk.SetPixels(pixels);
            disk.Apply(false, true);
        }
    }
}
