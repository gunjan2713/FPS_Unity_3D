// GameUIManager.cs - UPDATED
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameUIManager : MonoBehaviour
{
    // UI Elements
    public Text healthText;
    public Text livesText;
    public Text coinsText;
    public Text scoreText;
    public Text keysInventoryText;
    public Text weaponNameText;
    public Text ammoText;
    public Image weaponIcon;
    public Image redKeyIcon;
    public Image blueKeyIcon;

    // Visual assets
    public Sprite pistolIcon;
    public Sprite machineGunIcon;

    // References
    private HealthController healthController;
    private WeaponManager weaponManager;
    private PlayerInventory playerInventory;
    private PlayerStats playerStats;
    private ScoreManager scoreManager;

    void Start()
    {
        // Find player if references aren't assigned
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            healthController = player.GetComponent<HealthController>();
            weaponManager = player.GetComponent<WeaponManager>();
            playerInventory = player.GetComponent<PlayerInventory>();
            playerStats = player.GetComponent<PlayerStats>();

            Debug.Log("GameUIManager found player with components: " +
                      "HealthController: " + (healthController != null) +
                      ", PlayerStats: " + (playerStats != null));
        }

        // Find score manager
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogWarning("ScoreManager not found in the scene!");
        }

        // Initialize all UI elements
        UpdateAllUI();
    }

    void Update()
    {
        // Update all UI elements every frame
        UpdateAllUI();
    }

    void UpdateAllUI()
    {
        UpdateHealthUI();
        UpdateLivesUI();
        UpdateWeaponUI();
        UpdateKeysUI();
        UpdateCoinsUI();
        UpdateScoreUI();
    }

    void UpdateHealthUI()
    {
        if (healthText != null && healthController != null)
        {
            healthText.text = "Health: " + Mathf.Round(healthController.GetCurrentHealth());
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null && playerStats != null)
        {
            int lives = playerStats.GetLives();
            livesText.text = "Lives: " + lives;
        }
    }

    void UpdateWeaponUI()
    {
        if (weaponManager != null)
        {
            bool isPistolEquipped = weaponManager.IsPistolEquipped();

            if (weaponNameText != null)
            {
                weaponNameText.text = isPistolEquipped ? "Pistol" : "Machine Gun";
            }

            if (weaponIcon != null)
            {
                weaponIcon.sprite = isPistolEquipped ? pistolIcon : machineGunIcon;
            }

            if (ammoText != null)
            {
                var currentWeapon = weaponManager.GetCurrentWeapon();
                if (currentWeapon != null)
                {
                    ammoText.text = currentWeapon.AmmoStatus();
                }
            }
        }
    }

    void UpdateKeysUI()
    {
        if (playerInventory != null)
        {
            if (keysInventoryText != null)
            {
                List<string> keys = playerInventory.GetKeys();
                keysInventoryText.text = "Keys: " + (keys.Count > 0 ? string.Join(", ", keys) : "None");
            }

            if (redKeyIcon != null)
            {
                redKeyIcon.enabled = playerInventory.HasKey("red_key");
            }

            if (blueKeyIcon != null)
            {
                blueKeyIcon.enabled = playerInventory.HasKey("blue_key");
            }
        }
    }

    void UpdateCoinsUI()
    {
        if (coinsText != null)
        {
            int totalCoins = 0;

            // First check ScoreManager
            if (scoreManager != null)
            {
                totalCoins = scoreManager.GetCoins();
                Debug.Log("UI showing coins from ScoreManager: " + totalCoins);
            }
            // Then PlayerStats if needed
            else if (playerStats != null)
            {
                totalCoins = playerStats.GetCoins();
                Debug.Log("UI showing coins from PlayerStats: " + totalCoins);
            }
            // Finally try PlayerInventory as fallback
            else if (playerInventory != null)
            {
                totalCoins = playerInventory.GetCoins();
                Debug.Log("UI showing coins from PlayerInventory: " + totalCoins);
            }

            coinsText.text = "Coins: " + totalCoins;
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null && scoreManager != null)
        {
            scoreText.text = "Score: " + scoreManager.GetScore();
        }
    }
}