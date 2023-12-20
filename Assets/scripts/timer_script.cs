using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timer_script : MonoBehaviour
{
    public car_controlle car;
    public static timer_script instance;
    [SerializeField] private float t;
    [Header("score")]
    [SerializeField] private float startTime;
    [SerializeField] private float currentTime;
    [SerializeField] public Image timeBar;
    

    public int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI highScoreText;

    [Header("combo counter")]
    public int combo;
    [SerializeField] GameObject comboBar;
    [SerializeField] Image comboTimerBar;
    [SerializeField] private TextMeshProUGUI comboText;
    private float comboTimer;
    [SerializeField] private float comboResetTime;

    [SerializeField] private GameObject boostradyImage;
    void Start()
    {
        instance = this;
        t = 10;
        currentTime = startTime;
        comboTimer = 0;
        score = 0;
        combo =0; 
        
        
    }

    
    void Update()
    {
        HighScore();
        scoreText.text = score.ToString();
        highScoreText.text = $"HS: {PlayerPrefs.GetInt("highScore", 0)}";
        TimeBar();
        ComboCounter();
        
        if(car_controlle.boostReady == true)
        {
            boostradyImage.SetActive(true);
        }
        else boostradyImage.SetActive(false);

        
       

    }
    

    public void ResetTimer()
    {
       
        currentTime = startTime;
        comboTimer = comboResetTime;
    }

    private void ComboCounter()
    {
        if(comboTimer <=0)
        {
            comboBar.SetActive(false);
            combo = 0;
        }
        else
        {
            comboBar.SetActive(true);
            comboTimer -= Time.deltaTime;
        }
        comboText.text = Convert.ToString(combo) ;
        comboTimerBar.fillAmount = Mathf.Clamp(comboTimer/comboResetTime, 0f, 1f);
    }

    private void TimeBar()
    {
        timeBar.fillAmount = Mathf.Clamp(currentTime / startTime, 0.0f, 1.0f);
        
        t -= Time.deltaTime;

        if (10 - t >= 10 && startTime > 15 && startTime < 31)
        {
            startTime -= 0.5f;
            t = 10;
        }

        if (currentTime <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else currentTime -= Time.deltaTime;
    }
    private void HighScore()
    {
        if (score > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", score);
        }

    }
}
