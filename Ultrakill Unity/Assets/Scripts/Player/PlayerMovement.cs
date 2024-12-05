using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;
  [SerializeField] private float slideSpeed = 10f;
  [SerializeField] private float slideDuration = 0.8f;
  [SerializeField] private float crouchHeight = 1f;
  [SerializeField] private float standHeight = 2f;
  [SerializeField] private float crouchSpeed = 2.5f;
    [SerializeField] private float wallJumpForce = 5f;
  [SerializeField] private float gravity = -20f;
    [SerializeField] private float maxWallJumps = 3;
  [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravityScale = 3f; 


    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isSliding = false;
    private bool isCrouching = false;
    private bool canJump = true;
    private int wallJumpCount = 0;

    public Transform groundCheck;
  [SerializeField] private float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Transform wallCheck;
    [SerializeField] private float wallDistance = 0.5f;
    public LayerMask wallMask; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        isTouchingWall = Physics.CheckSphere(wallCheck.position, wallDistance, wallMask);

        if (isGrounded)
        {
            velocity.y = -2f;
            wallJumpCount = 0;
            canJump = true; 
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        float currentSpeed = isCrouching ? crouchSpeed : moveSpeed;
        

        isGrounded = true;

        if (!isSliding)
        {
            controller.Move(move * moveSpeed * Time.deltaTime);
        }



        if (Input.GetButton("Jump") && isGrounded && canJump && !isCrouching)
        {
            if (isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * gravityScale);
                canJump = false;
            }
            else if (isTouchingWall && wallJumpCount < maxWallJumps)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                velocity += transform.forward * wallJumpForce;
                wallJumpForce++;
            }
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl) && move.magnitude > 0 && !isSliding)
        {
            StartCoroutine(PerformSlide(move));
        }


        HandleCrouch();
    }

    IEnumerator PerformSlide(Vector3 moveDirection)
    {
        isSliding = true;

        controller.height = crouchHeight;
        controller.center = new Vector3(0, crouchHeight / 2, 0);

        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            controller.Move(moveDirection.normalized * slideSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        controller.height = standHeight;
        controller.center = new Vector3(0, standHeight / 2, 0);
        
        isSliding = false;
    }

        void HandleCrouch()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {

                isCrouching = true;
                //controller.height = crouchHeight;
                controller.center = new Vector3(0f, 2f, 0f);
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
