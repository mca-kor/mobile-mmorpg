using UnityEngine;

namespace MobileMMORPG.Prototype
{
    // 손가락 ID별 역할을 시작 위치에서 고정한다. 화면 중앙을 넘어도 역할을 바꾸지 않는다.
    public sealed class PrototypeTouchState
    {
        private const int NoTouch = -1;
        private int moveId = NoTouch;
        private int lookId = NoTouch;
        private Vector2 lastLookPosition;
        public Rect SafeArea { get; private set; }
        public Vector2 MoveCenter { get; private set; }
        public float Radius { get; private set; }
        public Vector2 MoveAxis { get; private set; }
        public Vector2 LookDelta { get; private set; }
        public bool Looking => lookId != NoTouch;
        public bool OwnsMove(int id) => moveId != NoTouch && id == moveId;
        public bool OwnsLook(int id) => lookId != NoTouch && id == lookId;

        public void ReleaseMissing(bool movePresent, bool lookPresent)
        {
            if (!movePresent) End(moveId);
            if (!lookPresent) End(lookId);
        }

        public void Configure(Rect safeArea)
        {
            Reset();
            SafeArea = safeArea;
            Radius = Mathf.Min(safeArea.width, safeArea.height) * 0.14f;
            float inset = Radius * 1.4f;
            MoveCenter = new Vector2(safeArea.xMin + inset, safeArea.yMin + inset);
        }

        public void BeginFrame() => LookDelta = Vector2.zero;

        public void Begin(int id, Vector2 position)
        {
            if (id == NoTouch || id == moveId || id == lookId || !SafeArea.Contains(position)) return;
            if (position.x < SafeArea.center.x)
            {
                if (moveId != NoTouch || Vector2.Distance(position, MoveCenter) > Radius) return;
                moveId = id;
                MoveAxis = Vector2.ClampMagnitude((position - MoveCenter) / Mathf.Max(Radius, 1f), 1f);
            }
            else if (lookId == NoTouch)
            {
                lookId = id;
                lastLookPosition = position;
            }
        }

        public void Move(int id, Vector2 position)
        {
            if (id == NoTouch) return;
            if (id == moveId)
                MoveAxis = Vector2.ClampMagnitude((position - MoveCenter) / Mathf.Max(Radius, 1f), 1f);
            if (id == lookId)
            {
                LookDelta += position - lastLookPosition;
                lastLookPosition = position;
            }
        }

        public void End(int id)
        {
            if (id == NoTouch) return;
            if (id == moveId)
            {
                moveId = NoTouch;
                MoveAxis = Vector2.zero;
            }
            if (id == lookId)
            {
                lookId = NoTouch;
                LookDelta = Vector2.zero;
            }
        }

        public void Reset()
        {
            moveId = lookId = NoTouch;
            MoveAxis = LookDelta = Vector2.zero;
            lastLookPosition = Vector2.zero;
        }
    }
}
