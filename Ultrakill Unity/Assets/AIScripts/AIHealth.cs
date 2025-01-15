using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHealth : MonoBehaviour
{
  
  public int health5;
  public void TakeDamageAi(int damage)
  {
    health5 -= damage;
    if (health5 < 0)
    {
        Destroy(gameObject);
        
    }
  }
}
