using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform respawnPos;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered teleporter!");

            // Teleport player to respawn position
            other.transform.position = new Vector3(respawnPos.position.x, respawnPos.position.y, respawnPos.position.z);

            Debug.Log("Player teleported to: " + respawnPos.position);
        }
    }
}
