using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shotgun : Gun_attributes, IGun_interface
{
    
    private Gun_movement gunMovement;
    static public Shotgun instance;


    [SerializeField] int bulletNumber;

    private void Awake()
    {
        gunMovement = Object.FindObjectOfType<Gun_movement>();
    }

    private void OnEnable()
    {
        gunMovement.setValues(moveToDefSpeed, defRot);
        gunMovement.AdjustPosRotForGun_OnSwitch(defPos, defRot);
        canShoot = true;
    }
    void Start()
    {
        instance = this;
       
        bulletInMag = magSize;
    }


    void Update()
    {
        ADS();
        if (Input.GetMouseButtonDown(0)) { Shoot(); }
        if (Input.GetKeyDown(KeyCode.R)) { ReloadCheck(); }
        

        if (!isReloading)
        {
            magTextUI.text = bulletInMag.ToString() + "/" + magSize.ToString();

        }
        else
        {
            magTextUI.text = "RELOADING " + bulletInMag.ToString() + "/" + magSize.ToString();
        }
    }

    public void ADS()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gunMovement.AdjustPosRotForADS(ADSPos, ADSRot);
        }
        if (Input.GetMouseButtonUp(1))
        {
            gunMovement.AdjustPosRotForADS(defPos, defRot);
        }
    }

    public IEnumerator onReload()
    {
        for(int i = bulletInMag +1; i <= magSize;i++)
        {
      
           yield return new WaitForSeconds(reloadTime);

           sound_manager.instance.PlayShotgunShellLoad();

           bulletInMag = i;
            
                        
        }

        StartCoroutine(sound_manager.instance.PlayShotgunClick(0.4f));
        canShoot = true;
        isReloading = false;
    }

    public void Reload()
    {
        isReloading = true;
        canShoot = false;
        StartCoroutine(onReload());
    }

    public void ReloadCheck()
    {
        if (bulletInMag != magSize ) { Reload(); }
    }

    public void Shoot()
    {
        if (canShoot && bulletInMag > 0)
        {
            if (isReloading)
            {
                Reload();
            }
            else
            {
                var newEffect = Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation);
                newEffect.transform.parent = gameObject.transform;

                for(int i = 1; i<=bulletNumber; i++)
                {
                    Instantiate(bullet, bulletSpwnPivot.transform.position, transform.rotation);
                    gunMovement.Recoil(recoilAmountX, recoilAmountY);
                }

                gunMovement.PushBack(pushBackForce);

                sound_manager.instance.PlayShotgunShot();
                Player_controller.instance.PushBack(Vector3.back, 300);

                bulletInMag--;

                canShoot = false;
                StartCoroutine(sound_manager.instance.PlayShotgunClick(0.4f));
                StartCoroutine(RateOfFire());
            }

        }
    }
    public IEnumerator RateOfFire()
    {
        yield return new WaitForSeconds(1 / rateOfFire);
        canShoot = true;
    }
}
