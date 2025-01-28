using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ChasePlayer : MonoBehaviour
{
   // public Transform AttackPlayer;
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    private NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
     player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
     nav = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
       moveToPlayer();
    }
   public void moveToPlayer()
    {
        //GetComponent(attackPlayer).enabled = false;
          transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
         nav.destination = player.position;
        
    }
    

}