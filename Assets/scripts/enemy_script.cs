using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class enemy_script : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    public static enemy_script instance;
    private car_controlle player;
    private bool isHit;
    private Rigidbody rb;
    public bool isDead;
    [SerializeField] private ParticleSystem deathParticle;
    [SerializeField] private float enemyDamage;
    [SerializeField] private AudioSource getHitSound;
    [SerializeField] private AudioSource carDamageSound;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        instance = this;
        isDead = false;  
        
        isHit = false;

        rb.freezeRotation = true;

        player = (car_controlle)FindAnyObjectByType(typeof(car_controlle));
    }

    void Update()
    {
        FollowTarget();
                
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isHit = true;
           
            if(!isDead) { OnContact(); } 
            
        }
    }
    private void OnContact()
    {
        if (isHit &&  player.velocity >6)
        {
            Die();
        }
        else if(player.velocity <3.5)
        {
            StartCoroutine(HitPlayer());
        }
        else  isHit = false;
    }

    private void Die()
    {
        isDead = true;
        rb.freezeRotation = false;

        timer_script.instance.OnEnemyKilled();

        CM_shake.instance.shakeCamera(1.3f, 0.2f);

        player.BoostCheck();

        Instantiate(deathParticle, transform.position, transform.rotation);

        getHitSound.Play();

        Destroy(gameObject,2);
    }
    private IEnumerator HitPlayer()
    {
        player_health.instance.TakeDamage(enemyDamage);
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(0, 0, -25);
        carDamageSound.Play();
        yield return new WaitForSeconds(0.5f);
        isHit = false;
    }
    private void FollowTarget()
    {     
        if (!isHit) 
        {     
            float step = enemySpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            transform.LookAt(player.transform);
        }
    }

  
}
