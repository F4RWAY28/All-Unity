using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoint : MonoBehaviour
{
    public GameObject player; // The player GameObject reference
    public string spawnPointName = "Spawn Point1"; // The name of the spawn point GameObject in the scene

    private void Start()
    {
        // Check if the player is assigned, if not find it by tag
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Ensure the player and spawn point are set up
        if (player == null)
        {
            Debug.LogError("Player GameObject not found! Ensure the player has the 'Player' tag.");
            return;
        }

        RespawnPlayer();
    }

    // This method is called when the player touches the VoidRespawn
    public void RespawnPlayer()
    {
        // Find the spawn point GameObject by name
        GameObject spawnPoint = GameObject.Find(spawnPointName);

        // If the spawn point is not found, log an error
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn Point GameObject not found! Ensure the spawn point exists in the scene.");
            return;
        }

        // Set the player's position to the spawn point's position
        player.transform.position = spawnPoint.transform.position;

        // Optionally, save the spawn position for future respawn or level transitions
        PlayerPrefs.SetFloat("SpawnPosX", spawnPoint.transform.position.x);
        PlayerPrefs.SetFloat("SpawnPosY", spawnPoint.transform.position.y);
        PlayerPrefs.SetFloat("SpawnPosZ", spawnPoint.transform.position.z);
    }
}
