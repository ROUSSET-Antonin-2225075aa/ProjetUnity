using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Ensure the enemy has the "Enemy" tag
        {
            // Destroy the enemy
            Destroy(other.gameObject);

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
