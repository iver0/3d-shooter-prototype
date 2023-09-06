using ShooterPrototype.Gameplay.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterPrototype.Characters.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private PlayerSettings settings;
        [SerializeField] private Transform cameraPivot;

        private Weapon _weapon;
        private CharacterController _characterController;
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private float _currentVerticalRotation;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _weapon = cameraPivot.GetComponentInChildren<Weapon>();
        }

        private void FixedUpdate()
        {
            var playerTransform = transform;
            var moveDirection = (
                playerTransform.forward * _moveInput.y + playerTransform.right * _moveInput.x
            ).normalized;
            _characterController.Move(moveDirection * (settings.moveSpeed * Time.fixedDeltaTime));
        }

        private void LateUpdate()
        {
            if (Cursor.lockState != CursorLockMode.Locked) return;

            var horizontalRotation =
                _lookInput.x
                * settings.mouseSensitivity
                * PlayerSettings.MOUSE_SENSITIVITY_MULTIPLIER;
            var verticalRotation =
                _lookInput.y
                * settings.mouseSensitivity
                * PlayerSettings.MOUSE_SENSITIVITY_MULTIPLIER;

            transform.Rotate(Vector3.up, horizontalRotation);

            _currentVerticalRotation -= verticalRotation;
            _currentVerticalRotation = Mathf.Clamp(
                _currentVerticalRotation,
                -PlayerSettings.VERTICAL_ROTATION_LIMIT,
                PlayerSettings.VERTICAL_ROTATION_LIMIT
            );

            cameraPivot.localEulerAngles = new Vector3(_currentVerticalRotation, 0f, 0f);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (Cursor.lockState != CursorLockMode.Locked) return;

            _moveInput = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            _lookInput = context.ReadValue<Vector2>();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (_weapon == null) return;
            if (!context.performed) return;
            if (Cursor.lockState != CursorLockMode.Locked) return;

            _weapon.Shoot();
        }

        public void OnReload(InputAction.CallbackContext context)
        {
            if (_weapon == null) return;
            if (!context.performed) return;

            _weapon.Reload();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !hasFocus;
        }
    }
}
