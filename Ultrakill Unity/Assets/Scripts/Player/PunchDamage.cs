using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchDamage : MonoBehaviour
{

    public int damage = 10;
    public LayerMask damageableLayer;

    void OnTriggerEnter(Collider other)
    {
        if ((damageableLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            Health targetHealth = other.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage); 
            }
        }
    }

}
