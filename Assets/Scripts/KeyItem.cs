using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public string keyID = "red_key";
    public float volume = 1.0f;

    // Reference to the AudioSource component
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // If no AudioSource is found, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("Added AudioSource component to key");
        }

        // Ensure audio settings
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player contacted key: " + keyID);

            // Add the key to player's inventory
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                // Add key to inventory
                inventory.AddKey(keyID);
                Debug.Log("Added key to inventory: " + keyID);

                // Play the sound using the AudioSource
                if (audioSource != null && audioSource.clip != null)
                {
                    Debug.Log("Playing key pickup sound: " + audioSource.clip.name);

                    // Make sure key remains active while sound plays
                    GetComponent<Renderer>().enabled = false;
                    GetComponent<Collider>().enabled = false;

                    // Play the sound
                    audioSource.Play();

                    // Destroy after sound finishes playing
                    Destroy(gameObject, audioSource.clip.length);
                }
                else
                {
                    Debug.LogWarning("AudioSource or AudioClip is missing!");
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.LogError("Player does not have a PlayerInventory component!");
            }
        }
    }
}