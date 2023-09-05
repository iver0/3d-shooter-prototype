using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterPrototype.Characters
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float mouseSensitivity = 1f;

        private CharacterController characterController;
        private Camera playerCamera;

        private Vector2 moveInput;
        private Vector2 lookInput;
        private float currentVerticalRotation;

        private const float MouseSensitivityMultiplier = 0.02f;
        private const float VerticalRotationLimit = 90f;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            playerCamera = Camera.main;
        }

        private void FixedUpdate()
        {
            var playerTransform = transform;
            var moveDirection = (playerTransform.forward * moveInput.y + playerTransform.right * moveInput.x).normalized;
            characterController.Move(moveDirection * (moveSpeed * Time.fixedDeltaTime));
        }

        private void LateUpdate()
        {
            if (Cursor.lockState != CursorLockMode.Locked) return;

            var horizontalRotation = lookInput.x * mouseSensitivity * MouseSensitivityMultiplier;
            var verticalRotation = lookInput.y * mouseSensitivity * MouseSensitivityMultiplier;

            transform.Rotate(Vector3.up, horizontalRotation);

            currentVerticalRotation -= verticalRotation;
            currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, -VerticalRotationLimit, VerticalRotationLimit);

            playerCamera.transform.localEulerAngles = new Vector3(currentVerticalRotation, 0f, 0f);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            lookInput = context.ReadValue<Vector2>();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !hasFocus;
        }
    }
}
