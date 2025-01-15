using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private bool isEnemyDeath = false;
    public int EnemiesKilled = 0;
    // Start is called before the first frame update nb
    void Start()
    {
        
    }
}
    // Update is called once per frameS
  /*void OnTriggerEnter(Collider other)
    {
        isEnemyDeath = true;
        if(isEnemyDeath == true)
        {
            Destroy(gameObject);
            EnemiesKilled++;
        }
}
*/