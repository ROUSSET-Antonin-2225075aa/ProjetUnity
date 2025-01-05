using UnityEngine;

public class ChestController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform lid; // Couvercle du coffre

    [Header("Lift Settings")]
    [SerializeField] private float openHeight = 2f; // Hauteur du couvercle lorsqu'il est ouvert
    [SerializeField] private float liftSpeed = 2f; // Vitesse du soulèvement
    [SerializeField] private float smoothingFactor = 0.1f; // Facteur de lissage pour rendre le mouvement plus fluide

    private Vector3 closedPosition; // Position initiale du couvercle
    private Vector3 targetPosition; // Position cible (ouverte ou fermée)
    private bool isOpen = false; // État du coffre
    private bool playerNearby = false; // Si le joueur est proche

    private void Start()
    {
        // Sauvegarde la position initiale du couvercle
        if (lid != null)
        {
            closedPosition = lid.position;
            targetPosition = closedPosition;
        }
    }

    private void Update()
    {
        // Si le joueur est proche et appuie sur E, bascule l'état du coffre
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            targetPosition = isOpen
                ? new Vector3(closedPosition.x, closedPosition.y + openHeight, closedPosition.z) // Soulève le couvercle
                : closedPosition; // Retourne à la position fermée
        }

        // Applique un déplacement fluide vers la position cible
        if (lid != null)
        {
            lid.position = Vector3.Lerp(lid.position, targetPosition, smoothingFactor * Time.deltaTime * liftSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Appuyez sur E pour ouvrir le coffre.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
