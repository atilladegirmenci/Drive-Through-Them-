using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner_script : MonoBehaviour
{
    public enemy_script enemy;
    [SerializeField] private float spawnCooldown;
    private float resetSpawnCooldwn;
     private enum spawnPointLocEnum
    {
        top,
        left,
        right,
        bottom
    }
    [SerializeField] private spawnPointLocEnum spawnPointLoc;
    
    void Start()
    {
        resetSpawnCooldwn = spawnCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnenmy();
    }
    private void SpawnEnenmy()
    {

        if (spawnCooldown<=0)
        {
            Instantiate(enemy, RandomLocGenerator(), enemy.transform.rotation);
            spawnCooldown = resetSpawnCooldwn;
        }
        else
        {
            spawnCooldown-= Time.deltaTime;

        }
    }
    private Vector3 RandomLocGenerator()
    {
        int x;
        switch (spawnPointLoc)
        {
            
            case spawnPointLocEnum.left:
               

                x = Random.Range(-24, 25);
                return new Vector3(x, 0.5f, 24);
                

            case spawnPointLocEnum.right:
                
                x = Random.Range(-24, 25);
                return new Vector3(x, 0.5f, -24);

            case spawnPointLocEnum.bottom:
                x = Random.Range(-24, 25);
                return new Vector3(-24, 0.5f, x);

            case spawnPointLocEnum.top:
                x = Random.Range(-24, 25);
                return new Vector3(24, 0.5f, x);
            default:
                return new Vector3(0, 0, 0);



        }


    }
}
