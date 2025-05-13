using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
    // Weapon characteristics
    public bool isPistol = true;              // Is this the pistol or machine gun?
    public float fireRate = 0.5f;             // Time between shots (pistol: slower, machine gun: faster)
    public float damage = 20f;                // Damage per shot (pistol: higher, machine gun: lower)
    public float range = 100f;                // Maximum range (pistol: shorter, machine gun: longer)
    public float accuracy = 0.02f;            // Accuracy factor (pistol: more accurate, machine gun: less accurate)
    public bool isAutomatic = false;          // Whether the weapon can fire automatically
    public int maxAmmo = 10;                  // Maximum ammo before reload (pistol: less, machine gun: more)
    public float reloadTime = 1.5f;           // Time to reload (pistol: faster, machine gun: slower)

    // Effects
    public GameObject decalPrefab;            // Bullet hole decal
    public AudioSource fireSound;             // Firing sound effect
    public AudioSource reloadSound;           // Reload sound effect
    public ParticleSystem muzzleFlash;        // Muzzle flash effect

    // Runtime variables
    private int currentAmmo;
    private bool isReloading = false;
    private float nextFireTime = 0f;

    // Decal management
    private GameObject[] bulletHoles;
    private int currentBulletHoleIndex = 0;
    private int maxBulletHoles = 20;

    void Start()
    {
        // Initialize bullet holes array
        bulletHoles = new GameObject[maxBulletHoles];

        // Start with full ammo
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        // Skip if reloading
        if (isReloading)
            return;

        // Check for reload
        if (Input.GetKeyDown(KeyCode.R) || currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // For automatic weapons, use GetButton (continuous), for semi-auto use GetButtonDown (single press)
        bool fireInput = isAutomatic ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0);

        // Fire the weapon
        if (fireInput && Time.time >= nextFireTime && currentAmmo > 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        // Set the time for the next shot based on fire rate
        nextFireTime = Time.time + fireRate;

        // Reduce ammo
        currentAmmo--;

        // Play muzzle flash
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Play fire sound
        if (fireSound != null)
        {
            fireSound.Play();
        }

        // Calculate bullet spread based on accuracy
        Vector3 spreadDirection = CalculateSpread();

        // Perform the raycast to detect hits
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, spreadDirection, out hit, range))
        {
            // Create bullet impact decal
            CreateBulletHole(hit);

            // Check for enemy hit
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    Vector3 CalculateSpread()
    {
        // Get the forward direction of the camera (where player is aiming)
        Vector3 direction = Camera.main.transform.forward;

        // Add random spread based on accuracy
        // Less accuracy (higher number) means more spread
        direction += new Vector3(
            Random.Range(-accuracy, accuracy),
            Random.Range(-accuracy, accuracy),
            Random.Range(-accuracy, accuracy)
        );

        // Normalize to ensure consistent range
        return direction.normalized;
    }

    void CreateBulletHole(RaycastHit hit)
    {
        // Destroy previous bullet hole if we've reached the maximum
        if (bulletHoles[currentBulletHoleIndex] != null)
        {
            Destroy(bulletHoles[currentBulletHoleIndex]);
        }

        // Create a new bullet hole
        bulletHoles[currentBulletHoleIndex] = Instantiate(
            decalPrefab,
            hit.point + hit.normal * 0.01f,
            Quaternion.FromToRotation(Vector3.forward, -hit.normal)
        );

        // Update the index for the next bullet hole
        currentBulletHoleIndex = (currentBulletHoleIndex + 1) % maxBulletHoles;
    }

    IEnumerator Reload()
    {
        // Start reloading
        isReloading = true;

        // Play reload sound
        if (reloadSound != null)
        {
            reloadSound.Play();
        }

        // Wait for reload time
        yield return new WaitForSeconds(reloadTime);

        // Refill ammo
        currentAmmo = maxAmmo;

        // Finish reloading
        isReloading = false;
    }

    // Property to get the current ammo state for UI display
    public string AmmoStatus()
    {
        return currentAmmo + " / " + maxAmmo;
    }
}