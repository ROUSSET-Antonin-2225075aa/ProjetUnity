using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public AudioClip collectSound; // Référence au clip audio à jouer

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
        // Notifie le GameManager
        GameManager.Instance.AddCoin();

        // Joue le son sans latence
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        // Détruit la pièce immédiatement
        Destroy(gameObject);
    }
}
