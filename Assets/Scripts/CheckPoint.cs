using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isActive = false;
    public GameObject activeVisual;  // Optional visual to show when checkpoint is active
    public AudioClip activationSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            // Activate this checkpoint
            ActivateCheckpoint(other.gameObject);
        }
    }

    void ActivateCheckpoint(GameObject player)
    {
        isActive = true;

        // Update player's respawn position
        HealthController healthController = player.GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.SetCheckpoint(transform.position);
        }

        // Play activation sound
        if (activationSound != null)
        {
            AudioSource.PlayClipAtPoint(activationSound, transform.position);
        }

        // Show active visual if available
        if (activeVisual != null)
        {
            activeVisual.SetActive(true);
        }

        Debug.Log("Checkpoint activated at: " + transform.position);
    }
}
