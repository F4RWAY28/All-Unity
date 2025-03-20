using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    // Reference to the Canvas Group to toggle visibility of the pause menu
    public CanvasGroup pauseMenuCanvasGroup;

    // Reference to the Timer Text UI object
    public GameObject timerTextObject;

    // Reference to the scripts you want to keep running
    public MonoBehaviour[] scriptsToKeepRunning;

    // Reference to the scripts you want to pause
    public MonoBehaviour[] scriptsToPause;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // Check if the pause menu canvas group is assigned
        if (pauseMenuCanvasGroup == null)
        {
            Debug.LogError("PauseGame: Pause menu canvas group is not assigned in the inspector!");
            return;
        }

        // Check if the Timer Text object is assigned
        if (timerTextObject == null)
        {
            Debug.LogError("PauseGame: Timer text object is not assigned in the inspector!");
            return;
        }

        // Make sure the game starts unpaused
        Time.timeScale = 1f;

        // Hide the pause menu at start
        pauseMenuCanvasGroup.alpha = 0f;
        pauseMenuCanvasGroup.interactable = false;
        pauseMenuCanvasGroup.blocksRaycasts = false;

        // Make sure TimerText is visible
        timerTextObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle pause when the 'Escape' key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pressed");
            TogglePause();
        }
    }

    // Function to toggle pause state
    void TogglePause()
    {
        if (isPaused)
        {
            Debug.Log("Unpausing game");

            // Unpause the game
            Time.timeScale = 1f;

            // Enable paused scripts
            foreach (var script in scriptsToPause)
            {
                script.enabled = true;
            }

            // Keep certain scripts running (manual control)
            foreach (var script in scriptsToKeepRunning)
            {
                script.enabled = true;
            }

            // Hide the pause menu
            pauseMenuCanvasGroup.alpha = 0f;
            pauseMenuCanvasGroup.interactable = false;
            pauseMenuCanvasGroup.blocksRaycasts = false;

        }
        else
        {
            Debug.Log("Pausing game");

            // Pause the game
            Time.timeScale = 0f;

            // Disable most scripts
            foreach (var script in scriptsToPause)
            {
                script.enabled = false;
            }

            // Keep specific scripts running (e.g., UI, inventory, etc.)
            foreach (var script in scriptsToKeepRunning)
            {
                script.enabled = true;
            }

            // Show the pause menu
            pauseMenuCanvasGroup.alpha = 1f;
            pauseMenuCanvasGroup.interactable = true;
            pauseMenuCanvasGroup.blocksRaycasts = true;
        }

        // Toggle paused state
        isPaused = !isPaused;
    }
}
