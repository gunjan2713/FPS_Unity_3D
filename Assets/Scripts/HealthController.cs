using UnityEngine;






using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float maxHealth = 200f;
    public float currentHealth;

    public Text healthText;
    public Text livesText;

    public AudioClip damageSound;
    public AudioClip deathSound;
    public AudioClip respawnSound;

    // Add specific volume controls
    public float deathSoundVolume = 1.0f;
    public float damageSoundVolume = 1.0f;
    public float respawnSoundVolume = 1.0f;

    private RespawnFeedback respawnFeedback;
    private PlayerStats playerStats;
    private Vector3 lastCheckpointPosition;
    private bool isRespawning = false;

    // Add dedicated audio source
    private AudioSource playerAudioSource;

    void Start()
    {
        // Initialize current health
        currentHealth = maxHealth;

        // Get reference to PlayerStats
        playerStats = GetComponent<PlayerStats>();
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats component not found on player! Adding one...");
            playerStats = gameObject.AddComponent<PlayerStats>();
        }
        else
        {
            Debug.Log("Found PlayerStats with " + playerStats.GetLives() + " lives");
        }

        // Save initial position as checkpoint
        lastCheckpointPosition = transform.position;

        // Set up dedicated audio source
        playerAudioSource = GetComponent<AudioSource>();
        if (playerAudioSource == null)
        {
            playerAudioSource = gameObject.AddComponent<AudioSource>();
        }
        playerAudioSource.playOnAwake = false;

        // Find respawn feedback
        respawnFeedback = FindObjectOfType<RespawnFeedback>();
        if (respawnFeedback == null)
        {
            Debug.LogWarning("RespawnFeedback component not found in scene!");
        }
        else
        {
            Debug.Log("Found RespawnFeedback component");
        }

        // Print debug info about sound assets
        Debug.Log("Sound clips assigned: " +
                  "Damage: " + (damageSound != null) + ", " +
                  "Death: " + (deathSound != null) + ", " +
                  "Respawn: " + (respawnSound != null));
    }

    public void TakeDamage(float damage)
    {
        if (isRespawning) return; // Don't take damage while respawning

        // Reduce current health by the damage amount
        currentHealth -= damage;
        print("Player health: " + currentHealth);

        // Play damage sound
        if (damageSound != null)
        {
            AudioSource.PlayClipAtPoint(damageSound, Camera.main.transform.position, damageSoundVolume);
        }

        // Check if the player's health has dropped to zero or below
        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        // Check if player has extra lives
        if (playerStats != null && playerStats.GetLives() > 0)
        {
            Debug.Log("Player died but has " + playerStats.GetLives() + " lives remaining");

            // Use a life and respawn
            playerStats.LoseLife();
            Debug.Log("After LoseLife, player now has " + playerStats.GetLives() + " lives");

            // Start respawn process which will play death sound ONCE
            StartRespawnProcess();
        }
        else
        {
            Debug.Log("Player died with no lives remaining. Game Over.");

            // Play death sound for final death - use multiple methods for reliability
            PlayFinalDeathSound();

            // No lives left, game over
            Die();
        }
    }

    // New method for final death sound
    private void PlayFinalDeathSound()
    {
        if (deathSound != null)
        {
            // Method 1: At camera position (most reliable for hearing it)
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);

            // Method 2: Create persistent sound object
            GameObject soundObj = new GameObject("FinalDeathSound");
            DontDestroyOnLoad(soundObj); // Keep it through scene transitions

            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = deathSound;
            source.volume = deathSoundVolume;
            source.spatialBlend = 0f; // Pure 2D sound
            source.priority = 0; // Highest priority
            source.Play();

            // Clean up after playing
            Destroy(soundObj, deathSound.length + 0.5f);

            Debug.Log("Final death sound playing: " + deathSound.name + " with volume " + deathSoundVolume);
        }
        else
        {
            Debug.LogError("Death sound is missing!");
        }
    }

    void StartRespawnProcess()
    {
        isRespawning = true;

        // Call respawn feedback if it exists
        if (respawnFeedback != null)
        {
            Debug.Log("Calling StartRespawnSequence on RespawnFeedback");
            respawnFeedback.StartRespawnSequence();
        }
        else
        {
            Debug.LogError("Cannot show respawn UI - RespawnFeedback not found!");
        }

        // Play death sound - USING MULTIPLE APPROACHES FOR RELIABILITY
        if (deathSound != null)
        {
            // Approach 1: Play at camera position for guaranteed audibility
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);

            // Approach 2: Play using player audio source
            if (playerAudioSource != null)
            {
                playerAudioSource.clip = deathSound;
                playerAudioSource.volume = deathSoundVolume;
                playerAudioSource.spatialBlend = 0f; // 2D sound
                playerAudioSource.Play();
            }

            // Approach 3: Create temporary dedicated audio source
            GameObject tempAudio = new GameObject("DeathSound");
            tempAudio.transform.position = Camera.main.transform.position;
            AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
            tempSource.clip = deathSound;
            tempSource.volume = deathSoundVolume;
            tempSource.spatialBlend = 0f; // 2D sound
            tempSource.Play();

            // Destroy after playing
            Destroy(tempAudio, deathSound.length + 0.5f);

            Debug.Log("Death sound playing using multiple methods: " + deathSound.name + " with volume " + deathSoundVolume);
        }
        else
        {
            Debug.LogError("Death sound is not assigned!");
        }

        // Disable controls temporarily
        DisablePlayerControls();

        // Make the player invisible or show death effect
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        // After a delay, respawn
        Invoke("Respawn", 2f);
    }

    void Respawn()
    {
        // Restore health
        currentHealth = maxHealth;

        // Return to last checkpoint position
        transform.position = lastCheckpointPosition;

        // Re-enable controls
        EnablePlayerControls();

        // Make player visible again
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }

        // Play respawn sound
        if (respawnSound != null)
        {
            // Play at camera position for best audibility
            AudioSource.PlayClipAtPoint(respawnSound, Camera.main.transform.position, respawnSoundVolume);
            Debug.Log("Respawn sound playing: " + respawnSound.name);
        }

        isRespawning = false;
    }

    // Other methods remain the same...
    public void SetCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
        Debug.Log("Checkpoint set at: " + position);
    }

    void Die()
    {
        // Final death when no lives remain
        print("Player has died! Game Over!");

        // Disable player controls
        DisablePlayerControls();

        // Call game over
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }

    void DisablePlayerControls()
    {
        // Disable FPS controller
        var fpsController = GetComponent<UnityEngine.MonoBehaviour>();
        if (fpsController != null && fpsController.GetType().Name.Contains("Controller"))
        {
            fpsController.enabled = false;
        }

        // Disable weapon scripts
        var weapons = GetComponentsInChildren<MonoBehaviour>();
        foreach (var weapon in weapons)
        {
            if (weapon.GetType().Name.Contains("Weapon") ||
                weapon.GetType().Name.Contains("Shooter") ||
                weapon.GetType().Name.Contains("Gun"))
            {
                weapon.enabled = false;
            }
        }
    }

    void EnablePlayerControls()
    {
        // Re-enable FPS controller
        var fpsController = GetComponent<UnityEngine.MonoBehaviour>();
        if (fpsController != null && fpsController.GetType().Name.Contains("Controller"))
        {
            fpsController.enabled = true;
        }

        // Re-enable weapon scripts
        var weapons = GetComponentsInChildren<MonoBehaviour>();
        foreach (var weapon in weapons)
        {
            if (weapon.GetType().Name.Contains("Weapon") ||
                weapon.GetType().Name.Contains("Shooter") ||
                weapon.GetType().Name.Contains("Gun"))
            {
                weapon.enabled = true;
            }
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void AddHealth(float amount)
    {
        currentHealth += amount;

        // Cap health at maximum
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}