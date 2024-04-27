using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class effect_script : MonoBehaviour
{
    static public effect_script instance;
    private enum effectTypeEnum { smoke ,blood,boost}
    [SerializeField] private effectTypeEnum effectType;
    void Start()
    {
        instance = this;

        if(effectType == effectTypeEnum.smoke) { Destroy(gameObject, 3); }
        else if(effectType == effectTypeEnum.blood) { Destroy(gameObject, 4); }
        else if (effectType == effectTypeEnum.boost) {  Destroy(gameObject, 2); }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }   
}
