// ScoreManager.cs
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private int score = 0;
    private int coins = 0;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score increased by " + amount + ". Total: " + score);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log("Coins increased by " + amount + ". Total: " + coins);

        // Also increase score when collecting coins
        AddScore(amount * 10); // Each coin is worth 10 points

        // Check for player stats to potentially add lives
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.AddCoins(amount);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int GetCoins()
    {
        return coins;
    }
}