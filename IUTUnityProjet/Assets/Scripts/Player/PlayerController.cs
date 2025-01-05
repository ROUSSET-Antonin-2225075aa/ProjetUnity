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
    [SerializeField] private float lateralSpeed = 3f; // Vitesse de déplacement latéral
    [SerializeField] private float accelerationTime = 0.1f;
    [SerializeField] private float turningSpeed = 5f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Sound")]
    [SerializeField] private AudioClip footstepSound; // Le son des pas
    private AudioSource footstepSource; // Source audio pour jouer le son des pas

    [Header("Animations")]
    [SerializeField]
    private Animator _animator;

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

        // Ajout dynamique d'une AudioSource pour jouer le son des pas
        footstepSource = gameObject.AddComponent<AudioSource>();
        footstepSource.clip = footstepSound;
        footstepSource.loop = true; // Boucle le son
        footstepSource.playOnAwake = false;
    }

    private void Update()
    {
        InputManagement();
        Movement();
        HandleFootstepSound();
    }

    private void Movement()
    {
        CalculateSpeed();
        GroundMovement();
        Turn();
    }

    private void CalculateSpeed()
    {
        // Ajuste la vitesse cible en fonction des entrées de mouvement
        if (moveInput != 0 || turnInput != 0)
        {
            targetSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        }
        else
        {
            targetSpeed = 0;
        }

        // Lisser la transition de la vitesse actuelle vers la vitesse cible
        speed = Mathf.SmoothDamp(speed, targetSpeed, ref currentVelocity.z, accelerationTime);
    }

    private void GroundMovement()
    {
        // Calcul de la direction de déplacement par rapport à la caméra
        Vector3 move = new Vector3(turnInput * lateralSpeed, 0, moveInput * speed);
        move = camera.transform.TransformDirection(move);
        move.y = 0; // Ignorer l'axe Y pour un déplacement au sol

        // Appliquer la gravité
        verticalVelocity = VerticalForceCalculation();
        move.y = verticalVelocity;

        // Mettre à jour l'animation en fonction de la direction du mouvement
        _animator.SetFloat("forward", moveInput);
        _animator.SetFloat("sideways", turnInput);

        // Déplacer le personnage
        controller.Move(move * Time.deltaTime);
    }

    private void Turn()
    {
        if (Mathf.Abs(turnInput) > 0 || moveInput > 0)
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

    private void HandleFootstepSound()
    {
        if ((moveInput != 0 || turnInput != 0) && controller.isGrounded)
        {
            if (!footstepSource.isPlaying)
            {
                // Commence à jouer le son si ce n'est pas déjà le cas
                footstepSource.Play();
            }
        }
        else
        {
            if (footstepSource.isPlaying)
            {
                // Arrête le son si le joueur arrête de marcher
                footstepSource.Stop();
            }
        }
    }
}
