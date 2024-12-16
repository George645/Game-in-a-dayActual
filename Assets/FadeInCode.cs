using UnityEngine;
using UnityEngine.UI;

public class FadeInPanel : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup; // The CanvasGroup of the panel to fade in
    public float fadeDuration = 1f;     // Duration of the fade-in effect in seconds

    private bool isFading = false;

    void Start()
    {
        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.alpha = 0f; // Ensure the panel starts fully transparent
            panelCanvasGroup.gameObject.SetActive(false); // Disable the panel at the start
        }
    }

    public void StartFadeIn()
    {
        if (!isFading && panelCanvasGroup != null)
        {
            panelCanvasGroup.gameObject.SetActive(true); // Enable the panel before fading in
            StartCoroutine(FadeIn());
        }
    }

    private System.Collections.IEnumerator FadeIn()
    {
        isFading = true;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            panelCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        panelCanvasGroup.alpha = 1f; // Ensure it's fully opaque at the end
        isFading = false;
    }
}