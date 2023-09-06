using ShooterPrototype.Events.Channels;
using UnityEngine;

namespace ShooterPrototype.Gameplay.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponSettings settings;
        [SerializeField] private Transform bulletPoint;
        [SerializeField] private IntEventChannel ammoChangedChannel;
        [SerializeField] private IntEventChannel maxAmmoChangedChannel;

        private Animator _animator;
        private bool _canShoot = true;
        private int _ammo;
        private static readonly int shoot = Animator.StringToHash("Shoot");

        private int ammo
        {
            get => _ammo;
            set { _ammo = value; ammoChangedChannel.RaiseEvent(_ammo); }
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            ammo = settings.maxAmmo;
            maxAmmoChangedChannel.RaiseEvent(ammo);
        }

        public void Shoot()
        {
            if (!_canShoot) return;
            if (ammo <= 0) return;

            var bullet = Instantiate(
                settings.bulletPrefab,
                bulletPoint.transform.position,
                transform.rotation
            );

            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * settings.bulletSpeed);
            bullet.GetComponent<Bullet>().damage = settings.damage;

            ammo--;
            _animator.SetTrigger(shoot);
            _canShoot = false;
            Destroy(bullet, 5f);
        }

        public void RecoilFinish()
        {
            _canShoot = true;
        }

        public void Reload()
        {
            ammo = settings.maxAmmo;
        }
    }
}
