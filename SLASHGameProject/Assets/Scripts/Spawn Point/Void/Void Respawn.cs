using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidRespawn : MonoBehaviour
{
    // Reference to the SpawnPoint script
    public SpawnPoint spawnPointScript;

    // Detect when the player enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Call the Respawn function to teleport the player
            if (spawnPointScript != null)
            {
                spawnPointScript.RespawnPlayer();
            }
            else
            {
                Debug.LogWarning("SpawnPoint script reference is missing in VoidRespawn.");
            }
        }
    }
}
