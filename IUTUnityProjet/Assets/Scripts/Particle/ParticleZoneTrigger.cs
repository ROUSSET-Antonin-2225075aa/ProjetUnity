using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie que l'objet est le joueur
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("StartMenu"); // Charge la scène "StartMenu"
        }
    }
}
