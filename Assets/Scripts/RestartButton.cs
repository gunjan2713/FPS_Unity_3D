using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    void Start()
    {
        // Get the button component and add a click listener
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(RestartGame);
        }

        // Make sure the cursor is visible and not locked
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void RestartGame()
    {
        Debug.Log("Restarting game...");

        // Find GameManager and call restart if it exists
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.RestartGame();
        }
        else
        {
            // Fallback: Load the main scene directly
            SceneManager.LoadScene("fps_scene"); // Make sure to use your actual scene name
        }
    }
}