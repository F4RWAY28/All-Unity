using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimer : MonoBehaviour
{
    private Timer timerScript;

    private void Start()
    {
        // Find the Timer script in the scene (adjust the name if needed)
        timerScript = FindObjectOfType<Timer>();

        if (timerScript == null)
        {
            Debug.LogError("No Timer script found in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player touches the teleporter
        if (other.CompareTag("Player"))
        {
            if (timerScript != null)
            {
                timerScript.ResetTimer(); // Call the reset method
            }
            else
            {
                Debug.LogWarning("Timer script reference is missing!");
            }
        }
    }
}
