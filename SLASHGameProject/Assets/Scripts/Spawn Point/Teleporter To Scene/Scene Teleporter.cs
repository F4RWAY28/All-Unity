using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    // Name of the scene to load
    public string sceneToLoad;

    // Name of the spawn point object in the target scene
    public string spawnPointName;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player touches the teleporter
        if (other.CompareTag("Player"))
        {
            // Save the spawn point name to PlayerPrefs
            PlayerPrefs.SetString("SpawnPointName", spawnPointName);

            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
