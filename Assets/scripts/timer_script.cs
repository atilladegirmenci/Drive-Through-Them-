using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timer_script : MonoBehaviour
{
    public static timer_script instance;

    [SerializeField] private float startTime;
    [SerializeField] private float currentTime;
    [SerializeField] public Image timeBar;
    
    public int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI highScoreText;
    void Start()
    {
        instance = this;
        currentTime = startTime;
        score = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        HighScore();
        scoreText.text = score.ToString();
        highScoreText.text = $"HS: {PlayerPrefs.GetInt("highScore", 0)}";

        timeBar.fillAmount = Mathf.Clamp(currentTime / startTime, 0.0f, 1.0f);
        if (currentTime <=0) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else currentTime -= Time.deltaTime;
    }

    public void ResetTimer()
    {
        currentTime = startTime;
    }

    private void HighScore()
    {
        if (score > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", score);
        }

    }
}
