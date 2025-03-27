using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class Collect : MonoBehaviour
{
    public TextMeshProUGUI coinText; // Reference to the TextMeshPro text UI
    private int coinCount = 0; // Tracks how many coins have been collected

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object the player touched has the "Coin" tag
        if (other.CompareTag("Coin"))
        {
            coinCount++; // Increase coin count
            coinText.text = "Coins: " + coinCount; // Update the UI text

            Destroy(other.gameObject); // Remove the coin after collecting
        }
    }
}
