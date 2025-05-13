using UnityEngine;
using System.Collections;

public class PlayerRespawnManager : MonoBehaviour
{
    private Vector3 respawnPosition;
    private Quaternion respawnRotation;

    // References to key components
    private CharacterController characterController;
    private MonoBehaviour firstPersonController;
    private Camera playerCamera;

    void Start()
    {
        // Store initial position as first respawn point
        respawnPosition = transform.position;
        respawnRotation = transform.rotation;

        // Get references to components
        characterController = GetComponent<CharacterController>();

        // Find the First Person Controller script
        MonoBehaviour[] components = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour component in components)
        {
            if (component.GetType().Name.Contains("FirstPersonController"))
            {
                firstPersonController = component;
                break;
            }
        }

        // Find the player camera (it's usually a child of FPSController)
        playerCamera = GetComponentInChildren<Camera>();

        Debug.Log("PlayerRespawnManager initialized. Components found: " +
                 "CharacterController: " + (characterController != null) +
                 ", FirstPersonController: " + (firstPersonController != null) +
                 ", Camera: " + (playerCamera != null));
    }

    // Call this from checkpoints to set respawn position
    public void SetRespawnPoint(Vector3 position, Quaternion rotation)
    {
        respawnPosition = position;
        // Keep only Y rotation for clean respawn
        respawnRotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);

        Debug.Log("New respawn point set: " + position);
    }

    // Call this when player needs to respawn
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnProcess());
    }

    private IEnumerator RespawnProcess()
    {
        Debug.Log("Starting respawn process...");

        // Disable player control during respawn
        if (firstPersonController != null)
        {
            firstPersonController.enabled = false;
        }

        // Disable character controller to allow position change
        if (characterController != null)
        {
            characterController.enabled = false;
        }

        // Short pause
        yield return new WaitForSeconds(0.1f);

        // Move player to respawn point with a slight height offset to prevent ground clipping
        transform.position = respawnPosition + new Vector3(0, 0.5f, 0);
        transform.rotation = respawnRotation;

        // Fix camera rotation
        if (playerCamera != null)
        {
            Transform cameraTransform = playerCamera.transform;
            cameraTransform.localRotation = Quaternion.identity; // Reset to forward-facing
        }

        // Re-enable components
        if (characterController != null)
        {
            characterController.enabled = true;
        }

        // Short pause before re-enabling control
        yield return new WaitForSeconds(0.2f);

        if (firstPersonController != null)
        {
            firstPersonController.enabled = true;
        }

        Debug.Log("Player respawned at: " + transform.position);
    }
}