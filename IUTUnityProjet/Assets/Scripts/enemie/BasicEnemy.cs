using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicEnemy : MonoBehaviour
{
    public float patrolDistance = 10f;
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public Transform player;
    public Transform model; // Reference to the child object with the 3D model
    private Vector3 startPosition;
    private bool movingForward = true;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (PlayerInSight())
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        float distanceFromStart = Vector3.Distance(startPosition, transform.position);

        if (movingForward && distanceFromStart >= patrolDistance)
        {
            movingForward = false;
            TurnAround();
        }
        else if (!movingForward && distanceFromStart <= 0.1f)
        {
            movingForward = true;
            TurnAround();
        }

        Vector3 direction = movingForward ? Vector3.forward : Vector3.back;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void ChasePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);
    }

    private bool PlayerInSight()
    {
        return Vector3.Distance(transform.position, player.position) <= detectionRange;
    }

    private void TurnAround()
    {
        if (model != null)
        {
            model.Rotate(0, 180f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("StartMenu");
        }
    }
}
