// ExtraLifePickup.cs - Updated with reliable sound
using UnityEngine;

public class ExtraLifePickup : MonoBehaviour
{
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered ExtraLifePickup trigger: " + other.name + " with tag: " + other.tag);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered ExtraLifePickup trigger");

            // Play sound first with guaranteed playback
            PlaySoundWithGuarantee();

            // Find PlayerStats component on the player
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                Debug.Log("PlayerStats found, adding life");
                playerStats.AddLife();

                // Destroy pickup
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Player entered ExtraLifePickup trigger but has no PlayerStats component!");
            }
        }
    }

    // This method ensures sound plays even if the object is destroyed
    private void PlaySoundWithGuarantee()
    {
        if (pickupSound != null)
        {
            // Create a temporary GameObject for sound
            GameObject soundObject = new GameObject("LifePickupSound");
            soundObject.transform.position = transform.position;

            // Add AudioSource component
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.clip = pickupSound;
            audioSource.volume = 1.0f;
            audioSource.spatialBlend = 0.5f;  // Mix of 2D and 3D sound
            audioSource.priority = 0;  // Highest priority
            audioSource.Play();

            // Destroy after playing
            Destroy(soundObject, pickupSound.length + 0.1f);

            Debug.Log("Life pickup sound playing: " + pickupSound.name + " (Length: " + pickupSound.length + "s)");
        }
        else
        {
            Debug.LogWarning("No pickup sound assigned to life pickup!");
        }
    }
}