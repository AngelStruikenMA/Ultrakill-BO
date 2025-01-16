using UnityEngine;
using UnityEngine.AI;

public class monk : MonoBehaviour
{
    
    public float detectionRange = 10f;          
    public float attackRange = 2f;              
    public float attackCooldown = 1.5f;         
    public int attackDamage = 20;               

    private Transform player;                  
    private NavMeshAgent agent;                 
        private float attackTimer = 0f;             

   
    private Animator animator;                  

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found. Ensure the player is tagged 'Player'.");
        }

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        
       if (animator != null)
            {
                animator.SetTrigger("Chase");
            }
        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.position);

    
            if (distanceToPlayer <= attackRange)
            {
                agent.isStopped = true;
                TryAttackPlayer();
            }
            else
            {
                agent.isStopped = false;
            }
        }

        
        attackTimer += Time.deltaTime;
    }

    private void TryAttackPlayer()
    {
         if (animator != null)
            {
               
            
        if (attackTimer >= attackCooldown)
        {
            animator.SetTrigger("Attack");
            attackTimer = 0f;
             

        
            
                
            

            
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
            }
    }
        

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
