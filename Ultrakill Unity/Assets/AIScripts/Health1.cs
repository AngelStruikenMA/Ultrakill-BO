using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health1 : MonoBehaviour
{
  public int EnemiesKilled = 0;
  public int health5;
  public void TakeDamage(int damage)
  {
    health5 -= damage;
    if (health5 < 0)
    {
        Destroy(gameObject);
         EnemiesKilled++;
    }
  }
}
