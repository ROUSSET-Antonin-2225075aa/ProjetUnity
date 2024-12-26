using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the main game scene (replace "MainGame" with your scene name)
        SceneManager.LoadScene("Game");
    }
}
