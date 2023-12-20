using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repair_script : MonoBehaviour 
{
    repair_spawner_script spawner;
    public static repair_script instance; 
    player_health PlayerHealth;
    Rigidbody rb;
   
    
    void Start()
    {
        instance = this;
        spawner = repair_spawner_script.instance;
        PlayerHealth = player_health.instance;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {     
        rb.transform.Rotate(new Vector3(0,40*Time.deltaTime,0));
    }

   
    

    private void RepairCar()
    {
        
        if(PlayerHealth.currentHealth != PlayerHealth.maxHealth)
        {
            spawner.isSpawned = false;
            PlayerHealth.currentHealth = PlayerHealth.maxHealth;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            RepairCar();

        }
    }


}
