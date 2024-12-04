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
  [SerializeField] private float gravity = -9.81f;
  [SerializeField] private float jumpHeight = 2f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isSliding = false;
    private bool isCrouching = false;

    public Transform groundCheck;
  [SerializeField] private float groundDistance = 0.4f;
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

        float currentSpeed = isCrouching ? crouchSpeed : moveSpeed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        isGrounded = true;

        if (!isSliding)
        {
            controller.Move(move * moveSpeed * Time.deltaTime);
        }

        if (Input.GetButton("Jump") && isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

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

