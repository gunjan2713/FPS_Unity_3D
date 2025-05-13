using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public string requiredKeyID = "red_key"; // The key needed to open this door
    public bool isLocked = true; // Whether the door requires a key

    public Transform leftDoor;
    public Transform rightDoor;
    public Transform leftClosedLocation;
    public Transform rightClosedLocation;
    public Transform leftOpenLocation;
    public Transform rightOpenLocation;
    public float speed = 1.0f;

    private bool isOpen = false;
    private PlayerInventory playerInventory = null;

    void Update()
    {
        // If a player with inventory is nearby and the door is processing
        if (playerInventory != null)
        {
            if (isOpen)
            {
                // Open door animation
                if (leftDoor != null && leftOpenLocation != null)
                    leftDoor.position = Vector3.Lerp(leftDoor.position, leftOpenLocation.position, Time.deltaTime * speed);

                if (rightDoor != null && rightOpenLocation != null)
                    rightDoor.position = Vector3.Lerp(rightDoor.position, rightOpenLocation.position, Time.deltaTime * speed);
            }
            else
            {
                // Close door animation
                if (leftDoor != null && leftClosedLocation != null)
                    leftDoor.position = Vector3.Lerp(leftDoor.position, leftClosedLocation.position, Time.deltaTime * speed);

                if (rightDoor != null && rightClosedLocation != null)
                    rightDoor.position = Vector3.Lerp(rightDoor.position, rightClosedLocation.position, Time.deltaTime * speed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<PlayerInventory>();

            // Check if door is locked and player has the required key
            if (isLocked)
            {
                if (playerInventory != null && playerInventory.HasKey(requiredKeyID))
                {
                    // Player has the key, open the door
                    isOpen = true;
                    Debug.Log("Door unlocked with key: " + requiredKeyID);

                    // Optional: Display message to player
                    // UIManager.instance.ShowMessage("Door unlocked!");
                }
                else
                {
                    // Player doesn't have the key
                    Debug.Log("Door is locked! Need key: " + requiredKeyID);

                    // Optional: Display message to player
                    // UIManager.instance.ShowMessage("This door is locked. Find the " + requiredKeyID);
                }
            }
            else
            {
                // Door is not locked, open normally
                isOpen = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = false;
            playerInventory = null;
        }
    }
}