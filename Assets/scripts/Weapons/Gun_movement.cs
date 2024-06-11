using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;
using static UnityEngine.GraphicsBuffer;

public class Gun_movement : MonoBehaviour
{
   // [SerializeField] private GameObject crossHair;
    public Camera playerCamera;
    private Vector3 tempPos;
    private Quaternion tempRot;
    private float rotationX;
    private float rotationY;
    private float currentRecXPos;
    private float currentRecYPos;
    private int i;
    private Quaternion gunRot;
    [SerializeField] private bool ADSIng;
    

    [SerializeField] private Quaternion defaultRot;
    [SerializeField] private Vector3 defaultPos;
    
    [SerializeField] private float moveToDefSpeed;
    [SerializeField] private float rotateToDefSpeed;
    [SerializeField] private float rotationXLimit;
    [SerializeField] private float rotationYLimit;
    static public Gun_movement instance;

    void Start()
    {
        instance = this;
        ADSIng = false;
        
    }

    
    void Update()
    {
      
        StartCoroutine(moveToDefault()) ;
        GunRotation();  
    }
    public void setValues(float _moveToDefSpeed,Quaternion _gunRot)
    {
        moveToDefSpeed = _moveToDefSpeed;
        gunRot = _gunRot;
        
    }
    private void GunRotation()
    {

        rotationX += -Input.GetAxis("Mouse Y")*0.1f;
        rotationY += Input.GetAxis("Mouse X")*0.1f;
        rotationX = Mathf.Clamp(rotationX, -rotationXLimit, rotationXLimit);
        rotationY = Mathf.Clamp(rotationY, -rotationYLimit, rotationYLimit);

       
        if(isMouseMoving() && !ADSIng)
        {
            transform.localRotation = Quaternion.Euler(rotationX + gunRot.eulerAngles.x, rotationY +gunRot.eulerAngles.y, rotationY +gunRot.eulerAngles.z); ;
            
        }
        
    }

    public void Recoil(float x, float y)
    {
        if(!ADSIng)
        {
            currentRecXPos = (Random.Range(-0.1f,0.1f) / 2) * x;
            currentRecYPos = (Random.Range(-0.1f, 0.1f) / 2) * y;

            transform.localRotation = Quaternion.Euler(-Mathf.Abs(currentRecYPos) + gunRot.eulerAngles.x, -Mathf.Abs(currentRecXPos) + gunRot.eulerAngles.y, gunRot.eulerAngles.z);
        }
        else
        {
            currentRecXPos = (Random.Range(-0.1f, 0.1f) / 10) * x;
            currentRecYPos = (Random.Range(-0.1f, 0.1f)/*(Random.value - .5f)*/ / 10) * y;

            transform.localRotation = Quaternion.Euler(-Mathf.Abs(currentRecYPos) , -Mathf.Abs(currentRecXPos) , 0);
        }
        

    }

    public void PushBack(float pushBackForce)
    {
        if(!ADSIng)
        {
            transform.Translate(new Vector3(0, 0, -pushBackForce) * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(0, 0, -pushBackForce/3) * Time.deltaTime);
        }
        
    }
    public void AdjustPosRotForGun_OnSwitch(Vector3 pos, Quaternion rot)
    {
        transform.localPosition = pos;
        transform.localRotation = rot;
        defaultRot = transform.localRotation;
        defaultPos = transform.localPosition;
       
    }
    public void AdjustPosRotForADS(Vector3 ADSpos, Quaternion ADSrot)
    {

        i++;
        
        if(i%2==1) 
        {
            ADSIng = true;
            // crossHair.SetActive(true);
            playerCamera.fieldOfView -= 10;

            tempPos = transform.localPosition;
            tempRot = transform.localRotation;

            defaultRot = ADSrot;
            defaultPos = ADSpos;
            transform.localRotation = ADSrot;
            transform.localPosition = ADSpos;
            
        }
        else
        {
            ADSIng = false;
            
            playerCamera.fieldOfView += 10;

            defaultPos = tempPos;
            defaultRot = tempRot;
            transform.localPosition = tempPos;
            transform.localRotation = tempRot;
            
        }
       
    }


    public IEnumerator moveToDefault()
    {
        
            if (transform.localRotation != defaultRot && !isMouseMoving())
            {

                yield return new WaitForSeconds(0.1f);
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, defaultRot, rotateToDefSpeed * Time.deltaTime);

            }
            if (transform.localPosition != defaultPos)
            {

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, defaultPos, moveToDefSpeed * Time.deltaTime);
            }
        
    }

    private bool isMouseMoving()
    {
        if (Input.GetAxis("Mouse X") <= -0.1 || Input.GetAxis("Mouse X") >= 0.1 || Input.GetAxis("Mouse Y") <= -0.1 || Input.GetAxis("Mouse Y") >= 0.1)
        { return true; }
        else { return false; }

    }
}
