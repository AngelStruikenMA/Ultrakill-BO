using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float crouchSpeed = 2.5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;



    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f; 
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical"); 
        
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        
        controller.Move(move * moveSpeed * Time.deltaTime);
        
        isGrounded = true; 
        if (Input.GetButton("Jump") && isGrounded) 
        { 
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("Has jumped!");
        }

        velocity.y += gravity * Time.deltaTime; 
        controller.Move(velocity * Time.deltaTime);
    }
}
