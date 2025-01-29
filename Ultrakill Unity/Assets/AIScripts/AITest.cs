using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class AITest : MonoBehaviour

{
   
    [SerializeField]  private List<GameObject> Ai = new List<GameObject>();
    [SerializeField]  private List<GameObject> Ai2 = new List<GameObject>();
   
   
        [SerializeField] private int maxEnemy = 8;
        [SerializeField] private int Enemy = 0;
        [SerializeField] private int maxEnemy2 = 8;
        [SerializeField] private int Enemy2 = 0;
        [SerializeField] private int SecondEnemy = 0;
         public int EnemiesKilled = 0;
        
    public GameObject AIPrefab;
    // Start is called before the first frame update

    // Update is called once per frame
    
    void Update()
    {
        StartCoroutine(SpawnDelay());
        StartCoroutine(SpawnDelay2());
       
      
    }
    private void SpawnEnemy()
    {
        if( Enemy <= maxEnemy)
        {
            
         Instantiate(AIPrefab, Ai[Enemy].transform.position, quaternion.identity);
        
            Enemy++;
            
        }

    }
    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(3);
         if( Enemy <= maxEnemy)
        {
            
         Instantiate(AIPrefab, Ai[Enemy].transform.position, quaternion.identity);
        
            Enemy++;
            
        }
    }
          private IEnumerator SpawnDelay2()
    {
        yield return new WaitForSeconds(10);
         if( Enemy2 <= maxEnemy2)
        {
            
         Instantiate(AIPrefab, Ai2[Enemy2].transform.position, quaternion.identity);
        
            Enemy2++;
            
        }
         

    }
  

}
