using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // V�rifie que l'objet est le joueur
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("StartMenu"); // Charge la sc�ne "StartMenu"
        }
    }
}
