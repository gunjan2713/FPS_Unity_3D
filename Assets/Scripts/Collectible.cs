using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public AudioClip pickupSound;
    public float rotationSpeed = 100f;
    public GameObject pickupEffect;  // Optional particle effect

    protected virtual void Update()
    {
        // Rotate the collectible for visual appeal
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play pickup sound
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, 1f);
            }

            // Spawn pickup effect
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            // Apply the collectible's effect
            ApplyEffect(other.gameObject);

            // Destroy this collectible
            Destroy(gameObject);
        }
    }

    // Each collectible type will implement this differently
    protected abstract void ApplyEffect(GameObject player);
}