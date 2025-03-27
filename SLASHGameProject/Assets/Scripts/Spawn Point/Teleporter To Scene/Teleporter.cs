using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public Transform respawnPos;
    public GameObject playerGO;


    public void OnTriggerEnter(Collider other)
    {
        // Check if the player touches the teleporter
        if (other.CompareTag("Player"))
        {
            playerGO.transform.position = new Vector3(respawnPos.position.x, playerGO.transform.position.y, respawnPos.position.z);

            // Load the specified scene
            //SceneManager.LoadScene(sceneToLoad);
        }
    }
}
