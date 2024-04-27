using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngineInternal;
using static car_effect_script;

public class car_effect_script : MonoBehaviour
{

    [SerializeField] private Transform camPos;
    public ParticleSystem boostEffect;
    public car_controlle car;
    public TrailRenderer[] tireMark;
    public GameObject[] tires;
    [SerializeField] private ParticleSystem smokeEffect;
    private bool isRotating;
    [SerializeField] private float minSpeed;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;
    private float pitchFromCar;
    private AudioSource engineSound;
    [SerializeField] private GameObject flashScreen;
    private float cooldwn;

    public static car_effect_script instance;
   
    void Start()
    {
        cooldwn = 0.05f;
        instance = this;
        engineSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (car.isGrounded())
        {
            StartEmitter();
        }
        else stopEmitter();

        TireRotate();
        EngineSound();
        
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) { isRotating = true; }
        else { isRotating = false; }


    }

    private void TireRotate()
    {
        float tireRotateX = 1.5f * car.velocity;
       
        if (Input.GetKeyDown(KeyCode.A))
        {
            tires[0].transform.localRotation = Quaternion.Euler(tireRotateX, -30, 0);
            
            tires[1].transform.localRotation = Quaternion.Euler(tireRotateX, -30, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            tires[0].transform.localRotation = Quaternion.Euler(tireRotateX, 30, 0);

            tires[1].transform.localRotation = Quaternion.Euler(tireRotateX, 30, 0);
        }
        else if(Input.GetKeyUp(KeyCode.A)|| Input.GetKeyUp(KeyCode.D))
        {
            tires[0].transform.localRotation = Quaternion.Euler(tireRotateX, 0, 0);

            tires[1].transform.localRotation = Quaternion.Euler(tireRotateX, 0, 0);
        }
        else
        {
            foreach(GameObject t in tires)
            {   
                t.transform.Rotate(new Vector3(1.5f * car.velocity, 0, 0));
            }
        }
       
            
        if (car.velocity > 7 && isRotating)
        {
            if (cooldwn <= 0)
            {
                foreach (GameObject t in tires)
                {
                    Instantiate(smokeEffect, t.transform.position, transform.rotation);
                }
                cooldwn = 0.05f;
            }
            else
            {
               cooldwn -= Time.deltaTime;
            }

        }
    }
   

    void EngineSound()
    {
       
        pitchFromCar = car.velocity / 6f;

        if (car.velocity < minSpeed)
        {
            engineSound.pitch = minPitch;
        }

        if (car.velocity > minSpeed && car.velocity < car.maxSpeed)
        {
            engineSound.pitch = minPitch + pitchFromCar;
        }

        if (car.velocity > car.maxSpeed -1)
        {
            engineSound.pitch = maxPitch;
        }
    }
    
    private void StartEmitter()
    {
        
        foreach(TrailRenderer tiremark in tireMark)
        {
            tiremark.emitting = true;
        }
  
    }
    public IEnumerator BoostEffect()
    {

        var color = flashScreen.GetComponent<UnityEngine.UI.Image>().color;
        color = Color.blue;
        color.a = 0.6f;
        flashScreen.GetComponent<UnityEngine.UI.Image>().color = color;

        foreach (TrailRenderer tiremark in tireMark)
        {
            tiremark.startColor = Color.Lerp(Color.white,Color.blue,2);
        }

        yield return new WaitForSeconds(1);
        
        foreach (TrailRenderer tiremark in tireMark)
        {
            tiremark.startColor = Color.Lerp(Color.blue, Color.white, 2);   
        }
    }
    
    private void stopEmitter()
    {
        foreach(TrailRenderer tiremark in tireMark) { tiremark.emitting = false;}
    }
   
}
