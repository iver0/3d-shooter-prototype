using ShooterPrototype.Characters;
using UnityEngine;

namespace ShooterPrototype.Gameplay.Weapons
{
    public class Bullet : MonoBehaviour
    {
        public int damage { get; set; }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage);
            }
        }
    }
}
