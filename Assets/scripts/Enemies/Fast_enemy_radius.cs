using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fast_enemy_radius : MonoBehaviour
{


    void Start()
    {

    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && transform.parent.GetComponent<Fast_enemy>().isAlive)
        {
            DamagePlayer();
        }

    }
    public void DamagePlayer()
    {
        Player_health.instance.TakeDamage(transform.parent.GetComponent<Fast_enemy>().damage);
    }
}
