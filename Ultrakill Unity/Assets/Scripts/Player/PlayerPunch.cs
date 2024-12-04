using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    public Animator armAnimator;
    public float punchCooldown = 1f; 

    private bool canPunch = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canPunch)
        {
            StartCoroutine(PerformPunch());
        }
    }

    IEnumerator PerformPunch()
    {
        canPunch = false;

        armAnimator.SetTrigger("Punch"); 

        yield return new WaitForSeconds(punchCooldown);
        canPunch = true; 
    }
}
