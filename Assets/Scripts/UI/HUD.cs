using TMPro;
using UnityEngine;

namespace ShooterPrototype.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text ammoText;

        public int maxAmmo { get; set; }
        public void UpdateHealth(int newHealth)
        {
            healthText.text = $"Health: {newHealth}";
        }

        public void UpdateAmmo(int newAmmo)
        {
            ammoText.text = $"Ammo: {newAmmo}/{maxAmmo}";
        }
    }
}
