using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_health : MonoBehaviour
{
    [SerializeField] private float playerHealth;
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private GameObject gothitEffect;

    static public Player_health instance;  
    void Start()
    {
        instance = this;
    }

    
    void Update()
    {
        hp.text = $"HP: {playerHealth}";

        if (playerHealth <= 0)
        {
           StartCoroutine(MySceneManager.instance.OpenDeathScene()); 
        }

        if (gothitEffect.GetComponent<Image>().color.a > 0 && playerHealth >0)
        {
            Color color = gothitEffect.GetComponent<Image>().color;
            color.a -= 0.01f;
            gothitEffect.GetComponent<Image>().color = color;
        }
    }

    public void TakeDamage(float damage)
    {
        Color color = gothitEffect.GetComponent<Image>().color;
        color.a = 0.6f;
        gothitEffect.GetComponent<Image>().color = color;
        playerHealth -= damage;
    }
}
