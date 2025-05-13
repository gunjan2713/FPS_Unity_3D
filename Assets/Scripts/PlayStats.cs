// PlayerStats.cs - Updated coin handling
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int lives = 0;
    public int coins = 0;

    public AudioClip extraLifeSound;
    public AudioClip coinSound;
    private AudioSource playerAudioSource;
    void Start()
    {
        Debug.Log("PlayerStats initialized with " + lives + " lives and " + coins + " coins");
        playerAudioSource = GetComponent<AudioSource>();
        if (playerAudioSource == null)
        {
            playerAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Keep test keys for debugging
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Test: Adding a life with L key");
            AddLife();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Test: Losing a life with K key");
            LoseLife();
        }

        // Add coin test key
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Test: Adding a coin with C key");
            AddCoins(1);
        }
    }

    public void AddLife()
    {
        lives++;
        Debug.Log("AddLife called - Lives increased to: " + lives);

        // Play sound with guaranteed playback
        PlaySound(extraLifeSound, "Extra Life");
    }

    public void LoseLife()
    {
        lives--;
        Debug.Log("LoseLife called - Lives decreased to: " + lives);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log("PlayerStats.AddCoins called - Added " + amount + " coins. Total: " + coins);

        // Play coin sound
        PlaySound(coinSound, "Coin");

        // Auto convert coins to lives at threshold
        if (coins >= 100)
        {
            int lifesToAdd = coins / 100;
            coins %= 100; // Keep remainder

            for (int i = 0; i < lifesToAdd; i++)
            {
                AddLife();
            }
        }
    }

    private void PlaySound(AudioClip clip, string soundName)
    {
        if (clip != null)
        {
            // Method 1: Use the attached AudioSource
            if (playerAudioSource != null)
            {
                playerAudioSource.PlayOneShot(clip, 1.0f);
                Debug.Log(soundName + " sound playing via PlayerAudioSource: " + clip.name);
            }
            // Method 2: Fallback to PlayClipAtPoint
            else
            {
                AudioSource.PlayClipAtPoint(clip, transform.position, 1.0f);
                Debug.Log(soundName + " sound playing via PlayClipAtPoint: " + clip.name);
            }
        }
        else
        {
            Debug.LogWarning("No " + soundName + " sound clip assigned!");
        }
    }

    public int GetLives()
    {
        return lives;
    }

    public int GetCoins()
    {
        return coins;
    }
}