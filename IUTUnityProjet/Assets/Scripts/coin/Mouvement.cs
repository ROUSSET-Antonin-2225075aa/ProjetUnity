using UnityEngine;

public class Mouvement : MonoBehaviour
{
    public float rotationSpeed = 50f; // Vitesse de rotation
    public float bounceSpeed = 2f; // Vitesse du mouvement en hauteur
    public float tiltAngle = 15f; // Angle d'inclinaison
    public float maxYOffset = 0.5f; // Distance maximale au-dessus de la position initiale
    public float minYOffset = -0.2f; // Distance maximale en dessous de la position initiale

    private Vector3 initialPosition;
    private float bounceHeight; // Amplitude aléatoire du mouvement

    void Start()
    {
        // Sauvegarde la position initiale de la pièce
        initialPosition = transform.position;

        // Incline la pièce initialement
        transform.localRotation = Quaternion.Euler(tiltAngle, 0, 0);

        // Génère une amplitude aléatoire entre minYOffset et maxYOffset
        bounceHeight = Random.Range(minYOffset, maxYOffset);
    }

    void Update()
    {
        // Rotation autour de l'axe local X
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime, Space.Self);

        // Mouvement oscillant avec contrainte
        float newY = initialPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(initialPosition.x, Mathf.Clamp(newY, initialPosition.y + minYOffset, initialPosition.y + maxYOffset), initialPosition.z);
    }
}
