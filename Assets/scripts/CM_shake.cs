using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using static Unity.VisualScripting.Metadata;

public class CM_shake : MonoBehaviour
{
    public static CM_shake instance { get; private set; }
    private CinemachineVirtualCamera cmCamera;
    private float shakeTimer;
    private CinemachineBasicMultiChannelPerlin perlinNoise;
    private car_controlle player;
    public GameObject objecthit;
    public GameObject parentObjectHit;
   
    // Start is called before the first frame update
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = (car_controlle)FindAnyObjectByType(typeof(car_controlle));
        instance = this;
        cmCamera = GetComponent<CinemachineVirtualCamera>();
        perlinNoise = cmCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        //MakeTransparent();
        CamControl();
            if(shakeTimer <=0 )
            {
              perlinNoise.m_AmplitudeGain = 0;
            }
            else shakeTimer -= Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
    
    }
    public void shakeCamera(float intensity, float time)
    {
        perlinNoise.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
    private void CamControl()
    {
        if(Input.GetKey(KeyCode.Q)) 
        {
            var angleOffset = player.transform.eulerAngles.y - transform.eulerAngles.y;
            transform.RotateAround(player.transform.position, Vector3.up, 60*Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.E))
        {
            var angleOffset = player.transform.eulerAngles.y - transform.eulerAngles.y;
            transform.RotateAround(player.transform.position, Vector3.up, -60 * Time.deltaTime);

        }
        if(Input.GetKeyDown(KeyCode.Mouse1)) { Cursor.lockState = CursorLockMode.None; }

        transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 5);

    }
    //private void MakeTransparent()
    //{
    //    Vector3 direction = (transform.position - player.transform.position).normalized;
    //    Ray ray = new Ray(Camera.main.transform.position, direction);
    //    if(Physics.Linecast(transform.position,player.transform.position,out RaycastHit hit))
    //    {
    //        objecthit = hit.transform.gameObject;
    //        Renderer renderer = objecthit.GetComponent<Renderer>();
           
           
    //    }
    //}

    
}
