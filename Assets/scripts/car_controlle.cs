using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class car_controlle : MonoBehaviour
{
    private Rigidbody rb;
    public static car_controlle instace;
    
    [SerializeField] private GameObject restarttext;
    [Header("car attributes")]
    [SerializeField] private float thrust;
    [SerializeField] private float rotatPerSec;
    [SerializeField] public float maxSpeed;
    [SerializeField] public float velocity;
    public bool canBoost;

    [Header("box cast")]
    [SerializeField] private float maxDistance;
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private LayerMask ground;
    


    void Start()
    {
        instace = this;
        rb = GetComponent<Rigidbody>();
        canBoost = false;
    }


    void FixedUpdate()
    {    
        velocity = rb.velocity.magnitude;

        CarMoveement();
        StartCoroutine(Boost());
         
    }
    public void BoostCheck()
    {
        if (timer_script.instance.combo > 1 && timer_script.instance.combo % 5 == 0)
        {
            canBoost = true;
        }   
    }
    private IEnumerator Boost()
    {
       
        if (canBoost && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(car_effect_script.instance.BoostEffect());

            maxSpeed *= 1.5f;
            rb.mass *= 1.3f;

            rb.AddRelativeForce(0, 0, 10, ForceMode.Impulse);

            canBoost = false;

            yield return new WaitForSeconds(2);

            maxSpeed /= 1.5f;
            rb.mass /= 1.3f;  
        }

    }

    private void CarMoveement()
    {
        if(rb.velocity.magnitude >= maxSpeed ) 
        {
            rb.velocity =Vector3.ClampMagnitude(rb.velocity,maxSpeed);
        }
        if(isGrounded())
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddRelativeForce(0, 0, thrust, ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddRelativeForce(0, 0, thrust / -1.2f, ForceMode.Impulse);
            }
        }
        

        if(rb.velocity.magnitude>0.6f)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(new Vector3(0, 1, 0) *(rotatPerSec/ Mathf.Clamp(rb.velocity.magnitude/4,1,1.2f)) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(new Vector3(0, -1, 0) * (rotatPerSec / Mathf.Clamp(rb.velocity.magnitude/4, 1, 1.2f)) * Time.deltaTime);
            }
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }
    public bool isGrounded()
    {
        return (Physics.BoxCast(transform.position, boxSize, Vector3.down, transform.rotation, maxDistance, ground));
    }

    //private IEnumerator isFlipped()
    //{
    //    yield return new WaitForSeconds(5);
    //    if (!isGrounded() ) 
    //    {
    //        restarttext.SetActive(true);
    //        if(Input.GetKey(KeyCode.R))
    //        {
    //            restarttext.SetActive(false);
    //            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //        }
    //    }
    //    else restarttext.SetActive(false);
    //}
}
