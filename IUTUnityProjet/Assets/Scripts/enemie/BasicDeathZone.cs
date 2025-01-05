using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicDeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("StartMenu");
        }
    }
}
