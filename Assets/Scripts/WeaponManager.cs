using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject pistolObject;       // Reference to pistol GameObject
    public GameObject machineGunObject;   // Reference to machine gun GameObject

    public KeyCode switchWeaponKey = KeyCode.Q;  // Key to switch weapons

    private bool isPistolEquipped = true;  // Track which weapon is equipped
    private Shooter pistol;        // Reference to pistol script
    private Shooter machineGun;    // Reference to machine gun script

    void Start()
    {
        // Get the shooter components
        if (pistolObject != null)
            pistol = pistolObject.GetComponent<Shooter>();

        if (machineGunObject != null)
            machineGun = machineGunObject.GetComponent<Shooter>();

        // Enable pistol by default, disable machine gun
        SwitchToPistol();
    }

    void Update()
    {
        // Check for weapon switch input
        if (Input.GetKeyDown(switchWeaponKey))
        {
            ToggleWeapon();
        }

        // Alternative: number keys to select specific weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToPistol();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToMachineGun();
        }
    }

    void ToggleWeapon()
    {
        if (isPistolEquipped)
        {
            SwitchToMachineGun();
        }
        else
        {
            SwitchToPistol();
        }
    }

    void SwitchToPistol()
    {
        pistolObject.SetActive(true);
        machineGunObject.SetActive(false);
        isPistolEquipped = true;
    }

    void SwitchToMachineGun()
    {
        pistolObject.SetActive(false);
        machineGunObject.SetActive(true);
        isPistolEquipped = false;
    }

    // Methods for the UI to access weapon information
    public bool IsPistolEquipped()
    {
        return isPistolEquipped;
    }

    public Shooter GetCurrentWeapon()
    {
        return isPistolEquipped ? pistol : machineGun;
    }
}