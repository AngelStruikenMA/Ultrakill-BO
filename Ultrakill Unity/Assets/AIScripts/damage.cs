using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    public int DMG;
    float cooldown = 0f;
    private void OnCollisionEnter(Collision collision)
    {
        Health1 health5= collision.gameObject.GetComponent<Health1>();
        if (health5 != null)
        {
            health5.TakeDamage(DMG);
        }
    }
}
