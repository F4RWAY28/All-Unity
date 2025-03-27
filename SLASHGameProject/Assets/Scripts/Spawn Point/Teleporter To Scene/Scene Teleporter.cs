using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    public string sceneToLoad;    // Name of the scene to load
    public Transform spawnPoint;  // Reference to the spawn point Transform

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player touches the teleporter
        if (other.CompareTag("Player"))
        {
            // Load the target scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the spawn point is assigned
        if (spawnPoint != null)
        {
            // Move the player to the spawn point
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = spawnPoint.position;
                Debug.Log("Teleported to spawn point: " + spawnPoint.name);
            }
            else
            {
                Debug.LogError("Player not found!");
            }
        }
        else
        {
            Debug.LogError("Spawn point not assigned!");
        }

        // Unsubscribe from the event to prevent multiple calls
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
}
