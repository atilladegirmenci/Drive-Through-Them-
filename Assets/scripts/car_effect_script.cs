using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngineInternal;
using static car_effect_script;

public class car_effect_script : MonoBehaviour
{
    
  
    public car_controlle car;
    public TrailRenderer[] tireMark;
    public GameObject[] tires;
    [SerializeField] private ParticleSystem smokeEffect;
    private bool isRotating;
    [SerializeField] private float minSpeed;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;
    //private float currentSpeed;
    private float pitchFromCar;
     private AudioSource engineSound;

    private bool tiremarksFlag;

   
    void Start()
    {
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
        foreach(GameObject t in tires)
        {
            t.transform.Rotate(new Vector3(1.5f * car.velocity, 0,0 ))  ;
            if(car.velocity > 7 && isRotating)
            {
                Instantiate(smokeEffect, t.transform.position, transform.rotation);
            }
           
        }
    }

    void EngineSound()
    {
       // currentSpeed = car.velocity;
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
        if(tiremarksFlag) { return; }
        
        foreach(TrailRenderer tiremark in tireMark)
        {
            
            tiremark.emitting = true;
        }
  
    }
    private void stopEmitter()
    {
        foreach(TrailRenderer tiremark in tireMark) { tiremark.emitting = false;}
    }
}
