using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class AITest : MonoBehaviour

{
   
    [SerializeField]  private List<GameObject> Ai = new List<GameObject>();
    [SerializeField]  private List<GameObject> SecondSpawn = new List<GameObject>();
   
        [SerializeField] private int maxEnemy = 8;
        [SerializeField] private int Enemy = 0;
        [SerializeField] private int SecondEnemy = 0;
        [SerializeField] public int EnemiesKilled = 0;
    public GameObject AIPrefab;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
        SecondSpawn1();
      
    }
    private void SpawnEnemy()
    {
        if( Enemy <= maxEnemy)
        {
            
         Instantiate(AIPrefab, Ai[Enemy].transform.position, quaternion.identity);
        
            Enemy++;
            
        }

    }
    private void SecondSpawn1()
    {
       if (EnemiesKilled == 3)
       {
        Instantiate(AIPrefab, SecondSpawn[SecondEnemy].transform.position, quaternion.identity);
       }
    }
  

}
