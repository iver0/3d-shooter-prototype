using System;
using ShooterPrototype.Events.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace ShooterPrototype.Characters
{
    public class Health : MonoBehaviour
    {
        public UnityEvent characterDied;

        [SerializeField] private IntEventChannel healthChangedChannel;
        [SerializeField] private int maxHealth = 100;

        private int _value;

        public int value
        {
            get => _value;
            private set { _value = value; healthChangedChannel.RaiseEvent(_value); }
        }

        private void Start()
        {
            value = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            value -= damage;
            if (value <= 0)
            {
                characterDied.Invoke();
            }
        }

        public void Heal(int healAmount)
        {
            value = Mathf.Clamp(value + healAmount, 0, maxHealth);
        }
    }
}
