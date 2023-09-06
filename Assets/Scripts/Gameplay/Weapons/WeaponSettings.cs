using UnityEngine;

namespace ShooterPrototype.Gameplay.Weapons
{
    [CreateAssetMenu(menuName = "Settings/Weapon")]
    public class WeaponSettings : ScriptableObject
    {
        public GameObject bulletPrefab;
        public float bulletSpeed = 600f;
        public int damage = 10;
        public int maxAmmo = 10;
    }
}
