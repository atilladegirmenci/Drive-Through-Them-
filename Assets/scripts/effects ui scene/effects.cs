using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effects : MonoBehaviour
{
    private enum effectTypeEnum { muzzleFlash, blood ,bulletHole,explosion,spark}
    [SerializeField] private effectTypeEnum effectType;
    void Start()
    {
        if(effectType == effectTypeEnum.muzzleFlash) { Destroy_(1); }
        else if (effectType == effectTypeEnum.blood) { Destroy_(5); }
        else if (effectType == effectTypeEnum.bulletHole) { Destroy_(10); }
        else if(effectType == effectTypeEnum.explosion) { Destroy_(3); }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (effectType == effectTypeEnum.spark) 
        { 
          if ( transform.parent.GetComponent<Explosive_enemy>().isAlive == false)
            {
                Destroy_(0);
            }
                
        }
    
    }


    private void Destroy_(float t)
    {
        Destroy(gameObject, t);  
    }
}
