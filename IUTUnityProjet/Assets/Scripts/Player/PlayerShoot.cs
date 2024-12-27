using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform shootPoint;
    public float projectileSpeed = 20f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
{
    GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

    Rigidbody rb = projectile.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.velocity = shootPoint.forward * projectileSpeed;
    }

    Collider playerCollider = GetComponent<Collider>();
    Collider projectileCollider = projectile.GetComponent<Collider>();
    if (playerCollider != null && projectileCollider != null)
    {
        Physics.IgnoreCollision(playerCollider, projectileCollider);
    }

    Destroy(projectile, 5f);
}

}
