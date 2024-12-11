using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private float slideSpeed = 10f;
    [SerializeField] private float slideDuration = 0.8f;
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private float standHeight = 2f;

    private CharacterController controller; 
    private bool isSliding = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void ProcessSlide()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isSliding)
        {
            StartCoroutine(PerformSlide());
        }
    }

    private IEnumerator PerformSlide() 
    {
        isSliding = true;
        controller.height = crouchHeight;

        float elapsed = 0f;
        while (elapsed < slideDuration) 
        { 
            Vector3 moveDirection = transform.forward;
            controller.Move(moveDirection * slideSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        controller.height = standHeight;
        isSliding = false;
    }
}

