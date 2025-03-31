using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelText : MonoBehaviour
{
    [System.Serializable]
    public class SceneMessage
    {
        public string sceneName;  // Name of the scene
        public string message;    // Text to display
    }

    public List<SceneMessage> sceneMessages = new List<SceneMessage>(); // Editable list in Inspector
    public TextMeshProUGUI levelText;  // Assign your TextMeshPro UI element
    public float fadeDuration = 1.5f;  // Time for fade in/out
    public float displayDuration = 2.5f;  // How long the text stays visible

    private static bool hasDisplayedMessage = false;  // Ensures it only runs once per scene load

    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name; // Get current scene name

        // Reset the static variable when a new scene is loaded
        SceneManager.sceneLoaded += ResetMessageFlag;

        // If message has already been displayed, do nothing
        if (hasDisplayedMessage)
        {
            return;
        }

        string messageToShow = "";

        // Find matching message for the scene
        foreach (SceneMessage sceneMessage in sceneMessages)
        {
            if (sceneMessage.sceneName == currentScene)
            {
                messageToShow = sceneMessage.message;
                break;
            }
        }

        // If a message was found, show it
        if (!string.IsNullOrEmpty(messageToShow))
        {
            hasDisplayedMessage = true;  // Set flag to prevent re-triggering
            StartCoroutine(ShowText(messageToShow));
        }
    }

    private IEnumerator ShowText(string message)
    {
        levelText.text = message;
        levelText.alpha = 0; // Start invisible

        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            levelText.alpha = t / fadeDuration;
            yield return null;
        }
        levelText.alpha = 1;

        // Wait before fading out
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            levelText.alpha = 1 - (t / fadeDuration);
            yield return null;
        }
        levelText.alpha = 0;
    }

    private void ResetMessageFlag(Scene scene, LoadSceneMode mode)
    {
        hasDisplayedMessage = false; // Reset when a new scene is loaded
        SceneManager.sceneLoaded -= ResetMessageFlag; // Unsubscribe to avoid duplicates
    }
}
