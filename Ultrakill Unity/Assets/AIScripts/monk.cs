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

   
    public Animator animator;                  

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found. Ensure the player is tagged 'Player'.");
        }

    
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
        if (attackTimer >= attackCooldown)
        {
            attackTimer = 0f;

        
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }

            
            Health1 playerHealth = player.GetComponent<Health1>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
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
