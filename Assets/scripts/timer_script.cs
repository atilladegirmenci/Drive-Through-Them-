using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timer_script : MonoBehaviour
{
    public static timer_script instance;
    [Header("score")]
    [SerializeField] private float startTime;
    private float currentTime;
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
    void Start()
    {
        instance = this;
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
        comboText.text = combo.ToString() +"x";
        comboTimerBar.fillAmount = Mathf.Clamp(comboTimer/comboResetTime, 0f, 1f);
    }

    private void TimeBar()
    {
        timeBar.fillAmount = Mathf.Clamp(currentTime / startTime, 0.0f, 1.0f);
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
