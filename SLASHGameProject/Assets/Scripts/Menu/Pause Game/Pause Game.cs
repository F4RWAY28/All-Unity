using System.Collections;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public CanvasGroup pauseMenuCanvasGroup;
    public GameObject timerTextObject;
    public MonoBehaviour[] scriptsToKeepRunning;
    public MonoBehaviour[] scriptsToPause;

    public float fadeDuration = 0.5f; // Duration of fade effect
    private bool isPaused = false;

    void Start()
    {
        if (pauseMenuCanvasGroup == null)
        {
            Debug.LogError("PauseGame: Pause menu canvas group is not assigned!");
            return;
        }

        if (timerTextObject == null)
        {
            Debug.LogError("PauseGame: Timer text object is not assigned!");
            return;
        }

        Time.timeScale = 1f;
        pauseMenuCanvasGroup.alpha = 0f;
        pauseMenuCanvasGroup.interactable = false;
        pauseMenuCanvasGroup.blocksRaycasts = false;
        timerTextObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (isPaused)
        {
            Debug.Log("Unpausing game");
            StartCoroutine(FadePauseMenu(false));
            Time.timeScale = 1f;

            foreach (var script in scriptsToPause)
                script.enabled = true;

            foreach (var script in scriptsToKeepRunning)
                script.enabled = true;
        }
        else
        {
            Debug.Log("Pausing game");
            StartCoroutine(FadePauseMenu(true));
            Time.timeScale = 0f;

            foreach (var script in scriptsToPause)
                script.enabled = false;

            foreach (var script in scriptsToKeepRunning)
                script.enabled = true;
        }

        isPaused = !isPaused;
    }

    IEnumerator FadePauseMenu(bool fadeIn)
    {
        float startAlpha = pauseMenuCanvasGroup.alpha;
        float endAlpha = fadeIn ? 1f : 0f;
        float elapsedTime = 0f;

        pauseMenuCanvasGroup.interactable = fadeIn;
        pauseMenuCanvasGroup.blocksRaycasts = fadeIn;

        while (elapsedTime < fadeDuration)
        {
            pauseMenuCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        pauseMenuCanvasGroup.alpha = endAlpha;
    }
}
