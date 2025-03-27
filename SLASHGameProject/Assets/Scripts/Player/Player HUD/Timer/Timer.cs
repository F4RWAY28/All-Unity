using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class Timer : MonoBehaviour
{
    private float elapsedTime = 0f; // Timer value
    private bool isTimerRunning = false; // Tracks if the timer should run

    private TextMeshProUGUI timerText; // Reference to TextMeshPro component

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>(); // Get the TextMeshPro component on the object
        timerText.text = "Time: 0.00"; // Update the UI text
    }

    void Update()
    {
        // Check for specific keys to start the timer
        if (!isTimerRunning && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
                                Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftControl)))
        {
            isTimerRunning = true;
            elapsedTime = 0f; // Reset timer
        }

        // If timer is running, update the text
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime; // Increase time
            timerText.text = "Time: " + elapsedTime.ToString("F2"); // Update the UI text
        }
    }

    // Method to reset and stop the timer
    public void ResetTimer()
    {
        isTimerRunning = false;
        elapsedTime = 0f;
        timerText.text = "Time: 0.00"; // Update the UI text
        Debug.Log("Timer stopped and reset.");
    }
}
