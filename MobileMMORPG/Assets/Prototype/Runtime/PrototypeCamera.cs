using UnityEngine;
using UnityEngine.InputSystem;

namespace MobileMMORPG.Prototype
{
    [DefaultExecutionOrder(-100)]
    public sealed class PrototypeCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField, Min(0.1f)] private float distance = 6f;
        [SerializeField, Min(0f)] private float sensitivity = 0.15f;
        private float yaw;
        private float pitch = 25f;
        private bool rotating;
        private bool waitForRelease;
        private CursorLockMode previousLock;
        private bool previousVisible;
        private PrototypeTouchControls touchControls;

        public void SetTouchControls(PrototypeTouchControls controls) => touchControls = controls;

        public void SetTarget(Transform value)
        {
            target = value;
            UpdatePose();
        }

        private void Update()
        {
            Mouse mouse = Mouse.current;
            bool held = mouse != null && mouse.rightButton.isPressed;
            bool escape = Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame;
            if (!Application.isFocused || escape)
            {
                waitForRelease = true;
                ReleaseCursor();
                return;
            }
            if (touchControls != null && touchControls.Looking)
            {
                ReleaseCursor();
                RotateView(touchControls.LookDegrees);
                return;
            }
            if (!held)
            {
                waitForRelease = false;
                ReleaseCursor();
                return;
            }
            if (waitForRelease) return;
            if (!rotating)
            {
                previousLock = Cursor.lockState;
                previousVisible = Cursor.visible;
                rotating = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                return; // 포인터 잠금 순간의 이동량으로 시점이 튀는 현상 방지
            }
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                waitForRelease = true;
                ReleaseCursor();
                return;
            }
            // Mouse.delta는 프레임 이동량이므로 deltaTime을 다시 곱하지 않는다.
            Vector2 delta = mouse.delta.ReadValue();
            RotateView(delta * sensitivity);
        }

        private void RotateView(Vector2 degrees)
        {
            yaw = Mathf.Repeat(yaw + degrees.x, 360f);
            pitch = Mathf.Clamp(pitch - degrees.y, -10f, 65f);
        }

        private void LateUpdate() => UpdatePose();

        private void UpdatePose()
        {
            if (target == null) return;
            Quaternion orbit = Quaternion.Euler(pitch, yaw, 0f);
            Vector3 pivot = target.position + Vector3.up * 1.2f;
            transform.SetPositionAndRotation(pivot - orbit * Vector3.forward * distance, orbit);
        }

        private void ReleaseCursor()
        {
            if (!rotating) return;
            Cursor.lockState = previousLock;
            Cursor.visible = previousVisible;
            rotating = false;
        }

        private void OnApplicationFocus(bool focused)
        {
            if (!focused)
            {
                waitForRelease = true;
                ReleaseCursor();
            }
        }

        private void OnDisable() => ReleaseCursor();
        private void OnDestroy() => ReleaseCursor();
    }
}
