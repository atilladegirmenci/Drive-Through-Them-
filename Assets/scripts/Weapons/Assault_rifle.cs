using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Assault_rifle : Gun_attributes,IGun_interface
{

    private Gun_movement gunMovement;
    static public Assault_rifle instance;

    private void Awake()
    {
        gunMovement = Object.FindObjectOfType<Gun_movement>();
    }
    private void OnEnable()
    {
        gunMovement.setValues(moveToDefSpeed,defRot);
        gunMovement.AdjustPosRotForGun_OnSwitch(defPos, defRot);
        canShoot = true;
    }
    void Start()
    {

        instance = this;
        canShoot = true;
        bulletInMag = magSize;
    }
   

    void Update()
    {
        ADS();
        Shoot();
        ReloadCheck();
        
        if(!isReloading)
        {
            magTextUI.text = bulletInMag.ToString() + "/" + magSize.ToString();
        }
        else
        {
            magTextUI.text = "RELOADING";
        }
        
    }
   

    public void Reload()
    {
        isReloading = true;
        canShoot = false;
        StartCoroutine(onReload());
       
    }

    public void Shoot()
    {

        if (Input.GetMouseButton(0) && canShoot && bulletInMag > 0)
        {
            if (isReloading)
            {
                Reload();
            }
            else
            {
                var newEffect = Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation);
                newEffect.transform.parent = gameObject.transform;

                Instantiate(bullet, bulletSpwnPivot.transform.position, transform.rotation);

                gunMovement.PushBack(pushBackForce);
                gunMovement.Recoil(recoilAmountX, recoilAmountY);

                sound_manager.instance.PlayARSound();
                
                bulletInMag--;



                canShoot = false;
                StartCoroutine(RateOfFire());
            }
           
           
        }
            
    }

    public void ADS()
    {
        if(Input.GetMouseButtonDown(1))
        {
            gunMovement.AdjustPosRotForADS(ADSPos, ADSRot);
        }
        if(Input.GetMouseButtonUp(1))
        {
            gunMovement.AdjustPosRotForADS(defPos, defRot);
        }
    }

    public IEnumerator onReload() 
    {
        sound_manager.instance.PlayARLoad();
        yield return new WaitForSeconds(reloadTime);
        bulletInMag = magSize;
        canShoot = true;
        isReloading = false;
    }
    public void ReloadCheck()
    {
       
        if (bulletInMag != magSize && Input.GetKeyDown(KeyCode.R)) { Reload(); }
    }

   
    public IEnumerator RateOfFire()
    {

       
        yield return new WaitForSeconds(1 / rateOfFire);
        canShoot = true;
    }

   
}
