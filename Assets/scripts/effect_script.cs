using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect_script : MonoBehaviour
{
   private enum effectTypeEnum { smoke ,blood}
    [SerializeField] private effectTypeEnum effectType;
    void Start()
    {
        if(effectType == effectTypeEnum.smoke) { Destroy(gameObject, 3); }
        else if(effectType == effectTypeEnum.blood) { Destroy(gameObject, 4); }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
