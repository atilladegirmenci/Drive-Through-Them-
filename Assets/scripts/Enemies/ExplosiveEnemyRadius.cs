using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosiveEnemyRadius : MonoBehaviour
{
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && transform.parent.GetComponent<Explosive_enemy>().isAlive)
        {
            
            transform.parent.GetComponent<Explosive_enemy>().Explode();
            gameObject.SetActive(false);
            DamagePlayer();
            Debug.Log("GAME LOST");
        }
    }
    public void  DamagePlayer()
    {
       Player_health.instance.TakeDamage(transform.parent.GetComponent<Explosive_enemy>().damage);
    }
}
