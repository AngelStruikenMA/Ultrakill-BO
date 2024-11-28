using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AITest : MonoBehaviour

{
   
    [SerializeField]  private List<GameObject> Ai = new List<GameObject>();
        [SerializeField] private int maxEnemy = 8;
        [SerializeField] private int Enemy = 0;

    public GameObject AIPrefab;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }
    private void SpawnEnemy()
    {
        if( Enemy <= maxEnemy)
        {
            
         Instantiate(AIPrefab, Ai[Enemy].transform.position, quaternion.identity);
        
            Enemy++;
            
        }
    }
  

}