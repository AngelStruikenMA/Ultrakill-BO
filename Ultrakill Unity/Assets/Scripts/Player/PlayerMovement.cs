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
    private bool isCrouching = false;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float standHeight = 2f;
    public float crouchHeight = 0.1f;

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

        float currentSpeed = isCrouching ? crouchSpeed : moveSpeed;
        controller.Move(move * currentSpeed * Time.deltaTime);
        
        isGrounded = true; 
        if (Input.GetButton("Jump") && isGrounded && !isCrouching) 
        { 
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            
        }

        velocity.y += gravity * Time.deltaTime; 
        controller.Move(velocity * Time.deltaTime);

        HandleCrouch();
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            
            isCrouching = true;
            //controller.height = crouchHeight;
            controller.center = new Vector3(0f, 2f,0f);
            //controller.center = new Vector3(0, crouchHeight / 2, 0);
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl)) 
        {
            
           isCrouching = false;
           // controller.height = standHeight;
            controller.center = Vector3.up;
            //controller.center = new Vector3(0, standHeight / 2, 0);

        }
    }
}
