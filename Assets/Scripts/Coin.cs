// CoinCollectible.cs - Updated with reliable sound handling
using UnityEngine;

public class CoinCollectible : Collectible
{
    public int value = 1;

    // Override ApplyEffect from the Collectible base class
    protected override void ApplyEffect(GameObject player)
    {
        // Play pickup sound with higher priority
        PlaySoundWithGuarantee();

        // Add coins to score manager
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddCoins(value);
            Debug.Log("Collected coin: +" + value);
        }
        else
        {
            // Fallback: Add directly to player components
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.AddCoins(value);
            }

            PlayerInventory inventory = player.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddCoins(value);
            }
        }
    }

    // This method ensures sound plays even if the object is destroyed
    private void PlaySoundWithGuarantee()
    {
        if (pickupSound != null)
        {
            // Create a temporary GameObject for sound
            GameObject soundObject = new GameObject("CoinSound");
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

            Debug.Log("Coin sound playing: " + pickupSound.name + " (Length: " + pickupSound.length + "s)");
        }
        else
        {
            Debug.LogWarning("No pickup sound assigned to coin!");
        }
    }
}