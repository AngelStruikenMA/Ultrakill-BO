using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float wallJumpForce = 5f;
    [SerializeField] private int maxWallJumps = 3;

    private int wallJumpCount = 0;
    [SerializeField]private bool isTouchingWall;
    private PlayerMovement playerMovement;

    public Transform wallCheck;
    [SerializeField] private float wallDistance = 0.5f;
    public LayerMask wallMask;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void ProcessJump()
    {
        isTouchingWall = Physics.CheckSphere(wallCheck.position, wallDistance, wallMask);

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("pressed");
            if (playerMovement.IsGrounded())
            {
                ApplyJumpForce();
                wallJumpCount = 0;
            }
            else if (isTouchingWall && wallJumpCount < maxWallJumps)
            {
                ApplyWallJumpForce();
                wallJumpCount++; 
            }
        }
    }

    private void ApplyJumpForce() 
    {
        Debug.Log("jump");
        Vector3 velocity = new Vector3(0, Mathf.Sqrt(jumpHeight *  -100f * Physics.gravity.y), 0);




        //Vector3 velocity = Vector3.up * 100f;

        GetComponent<CharacterController>().Move(velocity * Time.deltaTime);
    }

    private void ApplyWallJumpForce() 
    {
        Vector3 velocity = transform.forward * wallJumpForce;
        GetComponent<CharacterController>().Move(velocity * Time.deltaTime);
    }
  
}
