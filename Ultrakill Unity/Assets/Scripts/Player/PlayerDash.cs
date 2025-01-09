using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; 
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f; 

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isDashing = false;
    private float dashTime;
    private float lastDashTime;

    [Header("Camera Settings")]
    public Transform cameraTransform;
    public float dashShakeIntensity = 0.1f;

    [Header("Stamina Settings")]
    public int maxDashes = 3;
    private int currentDashes;
    public float staminaRechargeTime = 3f;
    private float staminaRechargeTimer;

    [Header("UI")]
    public Slider staminaBar; 
    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentDashes = maxDashes;

        if(staminaBar != null)
        {
            staminaBar.maxValue = maxDashes;
            staminaBar.value = currentDashes;
        }
    }

    
    void Update()
    {
        if (!isDashing)
        {
            HandleMovement();
            HandleDashInput(); 
        }
        else 
        {
            DashMove(); 
        }
        RechargeStamina();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = (cameraTransform.forward * moveZ + cameraTransform.right  * moveX).normalized;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void HandleDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentDashes > 0)
        {
            StartDash(); 
        }
    }

    void StartDash()
    {
        isDashing = true; 
        dashTime = Time.time + dashDuration;
        lastDashTime = Time.time;

        currentDashes--; 

        if (staminaBar != null)
        {
            staminaBar.value = currentDashes;
        }
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

    void RechargeStamina()
    {
        if (currentDashes < maxDashes)
        {
            staminaRechargeTimer += Time.deltaTime; 

            if(staminaRechargeTimer >= staminaRechargeTime)
            {
                currentDashes++;
                staminaRechargeTimer = 0;

                if(staminaBar != null)
                {
                    staminaBar.value = currentDashes;
                }
            }
        }
    }
}
