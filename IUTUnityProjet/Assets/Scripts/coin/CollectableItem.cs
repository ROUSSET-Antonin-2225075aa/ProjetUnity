using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    // This will be triggered when another collider enters this object’s trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with the collectable is the player
        if (other.CompareTag("Player"))
        {
            // Call a function to handle the collection
            CollectItem();
        }
    }

    // Function to handle collection of the item
    void CollectItem()
    {
        // Destroy the collectable object
        Destroy(gameObject);

        // You can add more logic here like updating score, inventory, etc.
        Debug.Log("Item Collected!");
    }
}
