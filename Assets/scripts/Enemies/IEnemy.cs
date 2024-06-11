using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy 
{
    void Die();
    void GetHeadDamage(float damage);
    
    void GetBodyDamage(float damage);
}
