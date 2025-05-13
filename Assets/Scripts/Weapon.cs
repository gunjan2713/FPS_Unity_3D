using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 5f;            // Rate of fire in shots per second
    public float damage = 10f;             // Damage inflicted by each shot
    public Transform firePoint;            // Point from which bullets are fired
    public GameObject bulletPrefab;        // Prefab of the bullet GameObject
    public bool canUse = false;            // Whether the weapon can be used
    public AudioSource fireSound;          // Sound played when the weapon is fired
    public Light spotlight;
    public Color firingColor = Color.red;  // Spotlight color when firing
    public Transform player;

    private float timeSinceLastShot;       // Time elapsed since the last shot

    private void Update()
    {
        // Update the time elapsed since the last shot
        timeSinceLastShot += Time.deltaTime;

        // Check if the weapon can be used and it's time to fire again
        if (canUse && timeSinceLastShot >= 1f / fireRate)
        {
            // Fire a bullet
            Fire();

            // Reset the time since the last shot
            timeSinceLastShot = 0f;
        }
    }

    public void Fire()
    {
        // Instantiate a new bullet GameObject at the fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Set the bullet damage if the Bullet script has a damage variable
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = damage;
        }

        // Set the spotlight color to red
        spotlight.color = firingColor;

        // Play the fire sound effect
        if (fireSound != null)
        {
            fireSound.Play();
        }
        player.GetComponent<HealthController>().TakeDamage(damage);

        // Destroy the bullet after a certain time (adjust the time to match bullet travel speed)
        Destroy(bullet, 2f);
    }
}
