using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("SLASH (Scene)"); // Loads the scene named "SLASH (Scene)"
    }
}
