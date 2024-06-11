using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosive_enemy : MonoBehaviour , IEnemy
{
    Rigidbody rb;
    private float maxHealth;

    private bool canExplode;
    public bool isAlive;
    private Player_controller player;
    public float damage;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource explosionSound;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private float enemySpeed;
    [SerializeField] private GameObject bodyPivot;
    [SerializeField] private ParticleSystem bloodEffect;
    [SerializeField] UnityEngine.UI.Image healthbar;
    [SerializeField] Canvas canvas;
    [SerializeField] private float health;
    [SerializeField] private float jumpOnDieAmount;

   // private bool forceSwitch;
    void Start()
    {
        player = (Player_controller)FindAnyObjectByType(typeof(Player_controller));
        attackSound.Play();
        isAlive = true;
        canExplode = true;
        rb= GetComponent<Rigidbody>();
        maxHealth = health;
    }

    void Update()
    {
        FollowAndLookPlayer();
       
        healthbar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);  
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Vector3 direction = -(bodyPivot.transform.position - collision.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            Instantiate(bloodEffect, collision.contacts[0].point, rotation);
        }
    }
   public  void Explode()
    {
        if(isAlive && canExplode) 
        {
            canExplode = false;
            transform.Find("head").gameObject.GetComponent<SphereCollider>().enabled = false;
            explosionSound.Play();
            Die();
            Instantiate(explosionEffect, bodyPivot.transform.position, Quaternion.identity);
            Player_controller.instance.PushBack( new Vector3(player.transform.position.x - transform.position.x,0.1f,player.transform.position.z-transform.position.z), 700);
            rb.AddForceAtPosition(new Vector3(Random.Range(-6.0f, 6.0f), jumpOnDieAmount, Random.Range(-6.0f, 6.0f)), bodyPivot.transform.position, ForceMode.Impulse);
        }
       
       
    }
    private void FollowAndLookPlayer()
    {
       
        if (isAlive)
        {
            transform.LookAt((player.transform));
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
        }

    }
    public void Die()
    {
       
        rb.freezeRotation = false;
        isAlive = false;
        attackSound.Stop();
        healthbar.gameObject.SetActive(false);

        Destroy(gameObject,5);

    }

    public void GetBodyDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            if(isAlive)
            {
                ScoreSystem.instance.UpdateScore();
            }
            
            Die();
        }
    }

    public void GetHeadDamage(float damage)
    {
        if (isAlive)
        {
            ScoreSystem.instance.UpdateScore();
        }

        Explode();
    }

    //private void FallDown()
    //{
    //    if (rb.velocity.y < -0.1)
    //    {
    //        transform.position -= new Vector3(0, 0.01f, 0);
    //    }
    //}

}
