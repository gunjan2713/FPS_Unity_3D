using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Static instance that can be accessed from other scripts
    public static GameManager instance;

    // Scene names - adjust these to match your actual scene names
    public string mainSceneName = "fps_scene";
    public string gameOverSceneName = "GameOver";

    // Player lives tracking
    public int playerLives = 3;
    public int maxLives = 5;  // Optional maximum lives cap

    private bool gameHasEnded = false;

    private void Awake()
    {
        // Singleton pattern to ensure only one GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager initialized and set to persist between scenes");
        }
        else
        {
            Debug.Log("Duplicate GameManager found, destroying this one");
            Destroy(gameObject);
        }
    }

    // Call this when player dies
    public void GameOver()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GameOver called - loading game over scene: " + gameOverSceneName);

            // Load the game over scene
            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    // Call this from your restart button
    public void RestartGame()
    {
        Debug.Log("RestartGame called - loading main scene: " + mainSceneName);

        // Reset game state
        gameHasEnded = false;

        // Load the main scene
        SceneManager.LoadScene(mainSceneName);
    }

    // Add this method to fix the error
    // Add these methods to your existing GameManager
    public void AddPlayerLife()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.AddLife();
        }
    }

    public bool HasExtraLives()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        return playerStats != null && playerStats.GetLives() > 0;
    }
    // You might also want a method to use a life when the player dies
    public bool UsePlayerLife()
    {
        playerLives--;

        Debug.Log("Player lost a life. Remaining lives: " + playerLives);

        // If no more lives, game over
        if (playerLives <= 0)
        {
            GameOver();
            return false;
        }

        // Return true if the player still has lives
        return true;
    }
}