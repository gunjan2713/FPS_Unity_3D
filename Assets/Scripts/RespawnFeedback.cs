using UnityEngine;
using UnityEngine.UI;

public class RespawnFeedback : MonoBehaviour
{
    public Image fadePanel;  // Black panel for screen fade
    public Text respawnText; // "Respawning..." text

    public float fadeDuration = 1.0f;

    private bool isFading = false;
    void Start()
    {
        // Make sure fade panel starts invisible
        if (fadePanel != null)
        {
            // Set initial alpha to 0 (invisible)
            Color color = fadePanel.color;
            color.a = 0;
            fadePanel.color = color;

            // Initially deactivate the panel
            fadePanel.gameObject.SetActive(false);
        }

        // Make sure respawn text starts inactive
        if (respawnText != null)
        {
            respawnText.gameObject.SetActive(false);
        }

        Debug.Log("RespawnFeedback initialized with Panel: " + (fadePanel != null) +
                  " and Text: " + (respawnText != null));
    }
    public void StartRespawnSequence()
    {
        // Show respawn text
        if (respawnText != null)
        {
            respawnText.gameObject.SetActive(true);
            respawnText.text = "Respawning...";
        }

        // Start fading to black
        StartFade(true);

        // After a delay, start fading back in
        Invoke("FadeBackIn", 1.5f);
    }

    void FadeBackIn()
    {
        // Hide respawn text
        if (respawnText != null)
        {
            respawnText.gameObject.SetActive(false);
        }

        // Fade back to clear
        StartFade(false);
    }

    void StartFade(bool fadeToBlack)
    {
        if (fadePanel != null)
        {
            isFading = true;

            // Set initial alpha
            Color color = fadePanel.color;
            color.a = fadeToBlack ? 0 : 1;
            fadePanel.color = color;

            // Make panel visible
            fadePanel.gameObject.SetActive(true);

            // Start fading
            StartCoroutine(FadeCoroutine(fadeToBlack));
        }
    }

    System.Collections.IEnumerator FadeCoroutine(bool fadeToBlack)
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = fadeToBlack ?
                Mathf.Lerp(0, 1, timer / fadeDuration) :
                Mathf.Lerp(1, 0, timer / fadeDuration);

            // Update panel alpha
            Color color = fadePanel.color;
            color.a = alpha;
            fadePanel.color = color;

            yield return null;
        }

        // If faded out completely, fade back in or hide panel
        if (!fadeToBlack)
        {
            fadePanel.gameObject.SetActive(false);
        }

        isFading = false;
    }
}