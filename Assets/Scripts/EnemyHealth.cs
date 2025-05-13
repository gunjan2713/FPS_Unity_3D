using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    // Collectible drop settings
    public GameObject[] possibleDrops;  // Array of collectible prefabs
    public float dropChance = 0.5f;     // 50% chance to drop something

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Drop a collectible
        DropCollectible();

        // Your existing death code (animations, score, etc.)

        // Destroy the enemy
        Destroy(gameObject);
    }

    void DropCollectible()
    {
        // Check if we should drop something
        if (Random.value <= dropChance && possibleDrops.Length > 0)
        {
            // Select a random drop from possible drops
            int dropIndex = Random.Range(0, possibleDrops.Length);
            GameObject collectiblePrefab = possibleDrops[dropIndex];

            // Instantiate the collectible at enemy position
            if (collectiblePrefab != null)
            {
                // Slightly above ground position
                Vector3 dropPosition = transform.position + Vector3.up * 0.5f;
                Instantiate(collectiblePrefab, dropPosition, Quaternion.identity);
                Debug.Log("Enemy dropped: " + collectiblePrefab.name);
            }
        }
    }
}