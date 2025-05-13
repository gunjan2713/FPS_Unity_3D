using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public Button restartButton;
    public Button mainMenuButton;

    void Start()
    {
        // Hide cursor during gameplay
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Add listeners to buttons
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
    }

    public void RestartGame()
    {
        // Reload the game scene
        SceneManager.LoadScene("fps_scene"); // Replace with your main game scene name
    }

    public void GoToMainMenu()
    {
        // Go back to main menu
        SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene name
    }
}