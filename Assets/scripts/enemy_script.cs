using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_script : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    public static enemy_script instance;
    private car_controlle player;
    private player_health playerHealth;
    private timer_script timer;
    private bool isHit;
    private Rigidbody rb;
    public bool hasCollided;
    [SerializeField] private ParticleSystem deathParticle;
    [SerializeField] private float enemyDamage;
    [SerializeField] private AudioSource getHitSound;
    [SerializeField] private AudioSource carDamageSound;
    
    void Start()
    {
        instance = this;
        hasCollided = false;
        timer = timer_script.instance;
        rb = GetComponent<Rigidbody>();
        isHit = false;
        playerHealth = (player_health)FindAnyObjectByType(typeof(player_health));
        player = (car_controlle)FindAnyObjectByType(typeof(car_controlle));
        rb.freezeRotation = true;
       
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
           
            if(!hasCollided) { StartCoroutine(DestroyEnemy()); } 
            
        }
    }
    private IEnumerator DestroyEnemy()
    {
        if (isHit&&  player.velocity >6)
        {
            hasCollided = true;
            rb.freezeRotation = false;
            
            timer.ResetTimer();
            timer.score++;
            timer.combo += 10;

            CM_shake.instance.shakeCamera(1.3f,0.2f);
            Instantiate(deathParticle, transform.position, transform.rotation);

            getHitSound.Play();
            
            yield return new  WaitForSeconds(2);
            
            Destroy(gameObject);
            isHit = false;
        }
        else if(player.velocity <3.5)
        {
            playerHealth.TakeDamage(enemyDamage);
            gameObject.GetComponent<Rigidbody>().AddRelativeForce(0, 0, -25);
            carDamageSound.Play();  
            yield return new WaitForSeconds(0.5f);
            isHit = false;
            
        }
        else  isHit = false;
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
