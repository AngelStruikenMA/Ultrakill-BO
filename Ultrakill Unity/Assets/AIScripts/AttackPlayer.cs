using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AttackPlayer : MonoBehaviour
{
    ChasePlayer chasePlayer;
    private NavMeshAgent nav;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void attackPlayer()
    {
        nav.SetDestination(transform.position);

       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.activeSelf)
        {
            attackPlayer();
        }
    }
}
