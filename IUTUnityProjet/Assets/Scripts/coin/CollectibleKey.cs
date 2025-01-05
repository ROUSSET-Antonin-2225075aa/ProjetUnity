using UnityEngine;

public class CollectibleKey : MonoBehaviour
{
    public GameObject KeyText; 
    private void Start()
    {
        KeyText.SetActive(false); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerKey playerKey = other.GetComponent<PlayerKey>();

            playerKey.canInteract = true;
            Debug.Log("Item Collected!");
            KeyText.SetActive(true);

            Destroy(gameObject); 
            
        }
    }
}
