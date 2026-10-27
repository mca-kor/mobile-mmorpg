using UnityEngine;
using UnityEngine.InputSystem;

namespace MobileMMORPG.Prototype
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PrototypeMotor : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float moveSpeed = 4f;
        [SerializeField, Min(0f)] private float turnSpeed = 720f;
        [SerializeField] private Transform view;
        private CharacterController controller;
        private float verticalSpeed;

        public void SetView(Transform cameraTransform) => view = cameraTransform;

        private void Awake() => controller = GetComponent<CharacterController>();

        private void Update()
        {
            Vector2 input = Vector2.zero;
            Keyboard keyboard = Keyboard.current;
            if (Application.isFocused && keyboard != null)
            {
                input.x = (keyboard.dKey.isPressed ? 1f : 0f) - (keyboard.aKey.isPressed ? 1f : 0f);
                input.y = (keyboard.wKey.isPressed ? 1f : 0f) - (keyboard.sKey.isPressed ? 1f : 0f);
            }
            Step(input, Time.deltaTime);
        }

        // 입력과 이동을 분리해 모바일 입력을 추가할 때도 같은 이동 규칙을 사용한다.
        public void Step(Vector2 input, float deltaTime)
        {
            if (deltaTime <= 0f) return;
            if (controller == null) controller = GetComponent<CharacterController>();
            Vector3 forward = view != null ? view.forward : Vector3.forward;
            forward.y = 0f;
            if (forward.sqrMagnitude < 0.0001f) forward = Vector3.forward;
            forward.Normalize();
            Vector3 right = Vector3.Cross(Vector3.up, forward);
            Vector2 clamped = Vector2.ClampMagnitude(input, 1f);
            Vector3 planar = right * clamped.x + forward * clamped.y;

            if (controller.isGrounded && verticalSpeed < 0f) verticalSpeed = -2f;
            verticalSpeed = Mathf.Max(verticalSpeed - 20f * deltaTime, -30f);
            CollisionFlags flags = controller.Move((planar * moveSpeed + Vector3.up * verticalSpeed) * deltaTime);
            if ((flags & CollisionFlags.Below) != 0) verticalSpeed = -2f;
            if (planar.sqrMagnitude > 0.0001f)
            {
                Quaternion facing = Quaternion.LookRotation(planar, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, facing, turnSpeed * deltaTime);
            }
        }
    }
}
