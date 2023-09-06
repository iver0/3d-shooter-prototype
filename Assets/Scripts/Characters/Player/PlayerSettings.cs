using UnityEngine;

namespace ShooterPrototype.Characters.Player
{
    [CreateAssetMenu(menuName = "Settings/Player")]
    public class PlayerSettings : ScriptableObject
    {
        public float moveSpeed = 10f;
        public float mouseSensitivity = 1f;

        public const float MOUSE_SENSITIVITY_MULTIPLIER = 0.02f;
        public const float VERTICAL_ROTATION_LIMIT = 90f;
    }
}
