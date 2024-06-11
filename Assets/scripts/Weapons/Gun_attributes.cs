using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun_attributes : MonoBehaviour
{
    public Vector3 defPos;
    public Quaternion defRot;
    public Vector3 ADSPos;
    public Quaternion ADSRot;
    public TextMeshProUGUI magTextUI;
    public bool canShoot;
    public float rateOfFire;
    [Range (0,25)]  public float recoilAmountX;
    [Range (0,25)]  public float recoilAmountY;
    public float pushBackForce;
    public int magSize;
    protected int bulletInMag;
    public GameObject bullet;
    public GameObject bulletSpwnPivot;
    public GameObject muzzle;
    public float moveToDefSpeed;
    public ParticleSystem muzzleFlash;
    protected bool isReloading;
    public float reloadTime;
    
}
