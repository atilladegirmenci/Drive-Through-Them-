using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repair_spawner_script : MonoBehaviour
{
    public static repair_spawner_script instance;
    public GameObject repair;
    public bool isSpawned;
    [SerializeField] private float RepairSpawnCooldown;
    [SerializeField] private float spawnCooldownReset;
    void Start()
    {
        
        RepairSpawnCooldown = spawnCooldownReset;
        instance = this;
        isSpawned = false;
    }

    
    void Update()
    {
        SpawnRepair();
    }

    private void SpawnRepair()
    {
        if (RepairSpawnCooldown <= 0)
        {
            isSpawned = true;
            RepairSpawnCooldown = spawnCooldownReset;
            Instantiate(repair, RepairSpawnLoc(), Quaternion.identity);
            Debug.Log("aaaaa");
        }
        if(!isSpawned)
        {
            RepairSpawnCooldown -= Time.deltaTime;
        }

    }
    private Vector3 RepairSpawnLoc()
    {
        int x = Random.Range(1, 4);
        switch (x)
        {
            case 1: return new Vector3(-7, 0.5f, - 16);

            case 2: return new Vector3(19, 0.5f, -6);

            case 3: return new Vector3(2, 0.5f, 13);

            default: return new Vector3(0, 0, 0);

        }
    }
}
