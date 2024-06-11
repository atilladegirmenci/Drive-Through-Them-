using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_controller : MonoBehaviour
{
  
    private Rigidbody rb;
    private CapsuleCollider playerCol;
    public Camera playerCamera;
    private float rotationXLimit = 75.0f;
    private float rotationX = 0;
    static public Player_controller instance;
   

    [Header("BOX CAST")]
    [SerializeField] private float maxDistance;
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private LayerMask ground;
    

    [Header("MOVEMENT ATTRIBUTE")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] public float lookSensivity;
    [SerializeField] private bool isMoving;
    [SerializeField] public bool isCrouched;
    [SerializeField] private bool isRunning;
    [SerializeField] private float forceToMove;
    [SerializeField] private float walksSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float fallDownSpeed;
    
    
    
    void Start()
    {
      
        instance = this;
        Application.targetFrameRate = 120;
        rb = GetComponent<Rigidbody>();
        playerCol = GetComponent<CapsuleCollider>();
       
    }

    
    void Update()
    {
        PlayerMovement();
        LookAround();
        Run();
        Jump();
        FallDown();
        Crouch();
        AdjustMaxSpeed();

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    private void PlayerMovement()
    {
       
         Vector3 xzVelo = new Vector3(rb.velocity.x, 0, rb.velocity.z);
         Vector3 yVelo = new Vector3(0,rb.velocity.y, 0);
         xzVelo = Vector3.ClampMagnitude(xzVelo, maxSpeed);
         rb.velocity = xzVelo + yVelo;


        if (rb.velocity.magnitude > 0) { isMoving = true; }
        else { isMoving = false; }

      
        
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddRelativeForce(Vector3.forward * forceToMove);
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.AddRelativeForce(Vector3.back * forceToMove);
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.AddRelativeForce(Vector3.right * forceToMove);
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.AddRelativeForce(Vector3.left * forceToMove);
            }
        
    }
   
    private void AdjustMaxSpeed()
    {
        if(!isRunning && !isCrouched)
        {
            maxSpeed = walksSpeed;
        }
        else if (isRunning && !isCrouched)
        {
            maxSpeed = runSpeed;
        }
        else if(!isRunning && isCrouched) 
        {
            maxSpeed = crouchSpeed;
        }
        else if(isRunning && isCrouched)
        {
            maxSpeed = crouchSpeed;
        }
    }
    public void PushBack(Vector3 direction , float magnitude)
    {
        rb.AddRelativeForce(direction * magnitude);
    }
    private  void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerCol.height = playerCol.height / 1.7f;
            isCrouched = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            playerCol.height = playerCol.height * 1.7f;
            isCrouched = false;
        }
        
    }
    private void Run()
    {
        if (isMoving && Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else if (Input.GetKeyUp (KeyCode.LeftShift))
        {
            isRunning = false;
        }
      
    }
    
    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded() && !isCrouched)
        {
            rb.AddRelativeForce(Vector3.up * jumpForce ,ForceMode.Impulse);
        }
    }
    private void LookAround()
    {
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSensivity, 0);

        rotationX += -Input.GetAxis("Mouse Y") * lookSensivity;
        rotationX = Mathf.Clamp(rotationX, -rotationXLimit, rotationXLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
   

    private void FallDown()
    {
        if(rb.velocity.y < -0.1 )
        {
            transform.position -=  new Vector3(0,fallDownSpeed,0);
        }
    }

    
    public bool isGrounded()
    {
        return (Physics.BoxCast(transform.position, boxSize, Vector3.down, transform.rotation, maxDistance, ground));
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }
}
