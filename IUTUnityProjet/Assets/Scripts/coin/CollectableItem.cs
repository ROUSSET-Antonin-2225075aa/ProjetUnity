using UnityEngine;

public class CollectableItem : MonoBehaviour
{
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

        // Détruit la pièce
        Destroy(gameObject);
    }
}
