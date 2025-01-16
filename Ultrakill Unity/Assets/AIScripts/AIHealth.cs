using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHealth : MonoBehaviour
{
  private Animator ani;
  
  public int health5;
  private void Start()
  {
    ani = GetComponent<Animator>();
  }
  public void TakeDamageAi(int damage)
  
  {
    if (ani != null)
            {
               
            
    health5 -= damage;
    if (health5 < 0)
    {
      
       ani.SetTrigger("Die");
        Destroy(gameObject, 10);
        
    }
  }
  }
}
