using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f; 

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isDashing = false;
    private float dashTime;
    private float lastDashTime;

    public Transform cameraTransform;
    public float dashShakeIntensity = 0.1f; 
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        if (!isDashing)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            moveDirection = (cameraTransform.forward * moveZ + cameraTransform.right * moveX).normalized;
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
            {
                StartDash();
            }
        }
        else 
        {
            DashMove(); 
        }

    }

    void StartDash()
    {
        isDashing = true; 
        dashTime = Time.time + dashDuration;
        lastDashTime = Time.time;
    }

    void DashMove()
    {
        Vector3 dashVelocity = moveDirection * dashSpeed; 
        controller.Move(dashVelocity * Time.deltaTime);

        cameraTransform.localPosition += Random.insideUnitSphere * dashShakeIntensity; 

        if (Time.time >= dashTime)
        {
            isDashing = false; 
            cameraTransform.localPosition = new Vector3(0, 1.8f, 0);
        }
    }
}
