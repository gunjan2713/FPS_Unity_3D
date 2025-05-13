using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;   // Speed of the bullet
    public float damage = 10f;  // Damage inflicted by the bullet

    // Update is called once per frame
    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has an EnemyHealth component
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            // Inflict damage on the enemy
            enemyHealth.TakeDamage(damage);
        }

        // Destroy the bullet when it collides with anything
        Destroy(gameObject);
    }
}
