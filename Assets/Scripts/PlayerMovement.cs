using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck; //fait référence au gameObjet "groundCheck"
    public float groundDistance = 0.4f; // le radius de la sphère qui permet de vérifier si on touche le sol
    public LayerMask groundMask; // pour controler ce que la sphère touche

    //jump
    public float jumpHeight = 3f;

    Vector3 velocity;
    bool isGrounded; // vérifie si on à toucher le sol ou non

    void Update()
    {
        //gere la gravité
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //recupère les inputs
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Vertical");

        //mouvements
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //saut du joueur
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


    }
}
