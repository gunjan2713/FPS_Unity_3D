using UnityEngine;

public class EnemyDisappear : MonoBehaviour
{
    // Function to handle collision with other objects
    private void OnTriggerEnter(Collider other)
    {
        print("Here");
        // Check if the object colliding with the enemy has a specific tag
        if (other.CompareTag("Bullet"))
        {
            // If the colliding object is a bullet, destroy the enemy
            print("Not a bullet");
            Destroy(gameObject);
        }
        else {
            print("Not a bullet");
            Destroy(gameObject);        }
    }
}
