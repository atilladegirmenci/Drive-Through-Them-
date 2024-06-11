using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fast_enemy : MonoBehaviour , IEnemy
{

    private GameObject player;
    private float maxHealth;
    private Rigidbody rb;
    public bool isAlive;
    private float cooldownReset;
    public float damage;
    [SerializeField] private AudioSource  moveSound;
    [SerializeField] private float moveCooldown;
    [SerializeField] private float radius;
    [SerializeField] private GameObject bodyPivot;
    [SerializeField] private ParticleSystem bloodEffect;
    [SerializeField] UnityEngine.UI.Image healthbar;
    [SerializeField] Canvas canvas;
    [SerializeField] private float health;
    
    void Start()
    {
        cooldownReset = moveCooldown;
        player = (GameObject.Find("Player"));
        rb = GetComponent<Rigidbody>();
        maxHealth = health;
        isAlive = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive) transform.LookAt(player.transform);
        healthbar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        
        MoveTowardsPlayer();
    }
    private void MoveTowardsPlayer()
    { 
        if(moveCooldown<=0 && isAlive)
        {
            
           transform.position = Vector3.MoveTowards(transform.position, transform.position + FindRandomPos(), 100);
           
            //transform.position += FindRandomPos();
           // transform.Translate(FindRandomPos());
            
            moveCooldown = cooldownReset;
            moveSound.Play();
        }
        else
        {
            moveCooldown -= Time.deltaTime;
        }
    }
    private Vector3 FindRandomPos()
    {
        Vector3 pos;
        float dirx;
        float dirz;
        //var vector2 = Random.insideUnitCircle.normalized * radius;
        //pos = new Vector3(vector2.x, transform.position.y, vector2.y);


        //int angle = Random.Range(-50, 50);
        //float x = Mathf.Sin(angle) * radius;
        //float z = Mathf.Cos(angle) * radius;
        //pos = new Vector3(x, transform.position.y, z);

        dirx = transform.position.x - player.transform.position.x;
        dirz =transform.position.z - player.transform.position.z;
        pos = new Vector3( Mathf.Clamp(-dirx,-1.3f,1.3f)*radius*Random.Range(0.5f,1f) , 0 , Mathf.Clamp(-dirz, -1.3f, 1.3f)*radius*Random.Range(0.5f, 1f));
        return pos; 
    }
    public void Die()
    {
        rb.freezeRotation = false;
        isAlive = false;

        Destroy(gameObject,5);
    }

    public void GetBodyDamage(float damage)
    {
        health -= damage;
        checkForHealth();
    }

    public void GetHeadDamage(float damage)
    {
        health -= damage*3;
        checkForHealth();
    }
    private void checkForHealth()
    {
        if (health <= 0)
        {
            if(isAlive)
            {
                ScoreSystem.instance.UpdateScore();
            }
            Die();
        }
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

}
