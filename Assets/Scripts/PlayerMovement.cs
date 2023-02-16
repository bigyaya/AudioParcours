using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public CharacterController controller;  // Référence au CharacterController
                                            // 
    [SerializeField] private float _normalSpeed = 12f; // Vitesse normale du joueur
    [SerializeField] private float _sprintSpeed = 20f; // Vitesse du joueur pendant le sprint

    public float gravity = -9.81f;// Gravité appliquée au joueur
    public Transform groundCheck;// Référence à l'objet représentant le sol
    public float groundDistance = 0.4f;// Rayon de la sphère utilisée pour détecter le sol  
    public LayerMask groundMask;// LayerMask indiquant les couches sur lesquelles la sphère doit détecter les collisions

    // Hauteur du saut
    public float jumpHeight = 3f;

    // Vélocité du joueur
    Vector3 velocity;

    // Booléen indiquant si le joueur touche le sol ou non
    bool isGrounded;

    private AudioSource _audioSource;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {


        // Vérifie si le joueur touche le sol
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Applique la gravité si le joueur n'est pas en train de sauter
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Récupère les inputs de déplacement horizontal et vertical
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calcule le vecteur de mouvement en fonction des inputs récupérés
        Vector3 move = transform.right * x + transform.forward * z;


        // Vérifie si le joueur appuie sur le bouton de sprint
        if (Input.GetButton("Sprint"))
        {
            // Si oui, utilise la vitesse de sprint pour calculer le vecteur de mouvement
            move *= _sprintSpeed;
            _audioSource.Play();
        }
        else
        {
            // Sinon, utilise la vitesse normale
            move *= _normalSpeed;
            _audioSource.Stop();
        }

        controller.Move(move * Time.deltaTime);// Applique le mouvement au joueur



        // Vérifie si le joueur appuie sur le bouton de saut et s'il touche le sol
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Applique une force pour faire sauter le joueur
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;// Applique la gravité au joueur      
        controller.Move(velocity * Time.deltaTime);// Applique la vélocité au joueur

        // Calcule la valeur de pitch en fonction de la vitesse normale
        float pitch = velocity.magnitude / _normalSpeed;

        // Applique la valeur de pitch à l'AudioSource du joueur 
        GetComponent<AudioSource>().pitch = pitch;
    }
}
