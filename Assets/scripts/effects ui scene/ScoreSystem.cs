using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    static public ScoreSystem instance;

    private int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    void Start()
    {
        instance = this;
        score = 0;
    }

    
    void Update()
    {
        scoreText.text = score.ToString();
    }
    public void UpdateScore()
    {
        score++;
        return;
    }
}
