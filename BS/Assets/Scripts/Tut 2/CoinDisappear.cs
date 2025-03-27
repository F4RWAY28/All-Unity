using UnityEngine;

public class CoinDisappear : MonoBehaviour
{
    public AudioClip coinSound; // Assign "Crash L Done Yes" in the Inspector
    private AudioSource audioSource;

    private void Start()
    {
        // Get or add an AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = coinSound;
        audioSource.playOnAwake = false; // Prevent it from playing immediately
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play the sound at the coin's position
            AudioSource.PlayClipAtPoint(coinSound, transform.position);

            // Destroy the coin immediately
            Destroy(gameObject);
        }
    }
}
