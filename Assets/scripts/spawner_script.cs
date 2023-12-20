using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner_script : MonoBehaviour
{
   
    public enemy_script enemy;
    
    [SerializeField] private float EnemySpawnCooldown;
    private float resetSpawnCooldwn;
   
    private enum spawnPointLocEnum
    {
        top,
        left,
        right,
        bottom,
       
            
    }
    [SerializeField] private spawnPointLocEnum spawnPointLoc;
    
    void Start()
    {
        
        
        resetSpawnCooldwn = EnemySpawnCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnenmy();
        

    }
    private void SpawnEnenmy()
    {

        if (EnemySpawnCooldown<=0)
        {
            Instantiate(enemy, RandomLocGenerator(), enemy.transform.rotation);
            EnemySpawnCooldown = resetSpawnCooldwn;
        }
        else
        {
            EnemySpawnCooldown-= Time.deltaTime;

        }
    }


    private Vector3 RandomLocGenerator()
    {
        int x;
        switch (spawnPointLoc)
        {
            
            case spawnPointLocEnum.left:
               

                x = Random.Range(-29, 30);
                return new Vector3(x, 0.5f, 29);
                

            case spawnPointLocEnum.right:
                
                x = Random.Range(-29, 30);
                return new Vector3(x, 0.5f, -29);

            case spawnPointLocEnum.bottom:
                x = Random.Range(-29, 30);
                return new Vector3(-29, 0.5f, x);

            case spawnPointLocEnum.top:
                x = Random.Range(-29, 30);
                return new Vector3(29, 0.5f, x);

            
            default:
                return new Vector3(0, 0, 0);



        }
       


    }
  
    
}
