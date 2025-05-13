using UnityEngine;

public class HealthPickup : Collectible
{
    public float healthAmount = 25f;

    protected override void ApplyEffect(GameObject player)
    {
        // Find health controller on player
        HealthController healthController = player.GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.AddHealth(healthAmount);
            Debug.Log("Picked up health: +" + healthAmount + " HP");
        }
    }
}