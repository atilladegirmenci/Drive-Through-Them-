using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player_health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float currentHealth;
    public Image healthBar;
    public GameObject gothitScreen;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0.0f, 1.0f);
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(gothitScreen.GetComponent<Image>().color.a > 0) 
        {
            var color = gothitScreen.GetComponent<Image>().color;
            color.a -= 0.01f;
            gothitScreen.GetComponent<Image>().color = color;
        }
    }

    
    public void TakeDamage(float damage)
    {
        var color = gothitScreen.GetComponent<Image>().color ;
        color.a = 0.6f;
        gothitScreen.GetComponent<Image>().color = color;
        currentHealth -= damage;
       
    }

   
}
