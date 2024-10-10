using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    [SerializeField] private Transform camera;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float accelerationTime = 0.1f;
    [SerializeField] private float turningSpeed = 5f;
    [SerializeField] private float gravity = 9.81f;

    private float speed;
    private float targetSpeed;
    private float verticalVelocity;
    private Vector3 currentVelocity = Vector3.zero;

    [Header("Input")]
    private float moveInput;
    private float turnInput;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        InputManagement();
        Movement();
    }

    private void Movement()
    {
        CalculateSpeed();
        GroundMovement();
        Turn();
    }

    private void CalculateSpeed()
    {
        // Vérifie si le joueur est en mode sprint ou marche et ajuste la vitesse cible
        targetSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        // Lisser le passage entre les vitesses de marche et de sprint
        speed = Mathf.SmoothDamp(speed, targetSpeed, ref currentVelocity.z, accelerationTime);
    }

    private void GroundMovement()
    {
        // Obtenir la direction de déplacement relative à la caméra
        Vector3 move = new Vector3(turnInput, 0, moveInput);
        move = camera.transform.TransformDirection(move);
        move.y = 0; // On ignore l'axe Y pour un déplacement au sol

        // Appliquer la vitesse et la gravité
        verticalVelocity = VerticalForceCalculation();
        move = move.normalized * speed;
        move.y = verticalVelocity;

        // Déplacer le personnage
        controller.Move(move * Time.deltaTime);
    }

    private void Turn()
    {
        if (Mathf.Abs(turnInput) > 0 || Mathf.Abs(moveInput) > 0)
        {
            // Calculer la direction actuelle de regard en fonction de la vitesse du controller
            Vector3 currentLookDirection = controller.velocity.normalized;
            currentLookDirection.y = 0;

            if (currentLookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed);
            }
        }
    }

    private float VerticalForceCalculation()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }

    private void InputManagement()
    {
        // Gestion des entrées de mouvement
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

    }
}
