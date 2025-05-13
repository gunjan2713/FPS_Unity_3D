using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    public Text ammoText;
    public Shooter activeWeapon;

    void Update()
    {
        if (activeWeapon != null && ammoText != null)
        {
            ammoText.text = activeWeapon.AmmoStatus();
        }
    }

    // Call this method when switching weapons
    public void UpdateActiveWeapon(Shooter newWeapon)
    {
        activeWeapon = newWeapon;
    }
}