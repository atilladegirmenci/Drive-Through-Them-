using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobSystem : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [Range(1f,30f)] public float frequency ;
    [Range(0.001f, 0.02f)] public float amount ;
    [Range(10f, 100f)] public float smooth ;
    private Player_controller player;

    Vector3 startPos;
    void Start()
    {
        player = transform.parent.transform.parent.GetComponent<Player_controller>();
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
       CheckForHeadBob();
       StopHeadBob();
    }

    private void CheckForHeadBob()
    {
        float inputMagnetude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;
        if (inputMagnetude > 0 && player.isGrounded()) 
        {
            StartHeadBob();
        }
    }

    private Vector3 StartHeadBob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * rb.velocity.magnitude * 1.4f, smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.y, Mathf.Cos(Time.time * frequency / 2f) * amount *rb.velocity.magnitude*  1.6f, smooth * Time.deltaTime);
        transform.localPosition = pos;

        return pos;
    }

    private void StopHeadBob()
    {
        if (transform.localPosition == startPos) { return; }
        transform.localPosition = Vector3.Lerp(transform.localPosition,startPos,1*Time.deltaTime);
            }
}
