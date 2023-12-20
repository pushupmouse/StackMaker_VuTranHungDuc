using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<UIManager>();
                if(instance == null)
                {
                    instance = new GameObject().AddComponent<UIManager>();
                }
            }
            return instance; 
        }
    }

    [SerializeField] public GameObject victoryPanel;
    [SerializeField] public GameObject pausePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI totalScoreText;

    private int score = 0;
    private int bonusScore = 0;
    private int totalScore = 0;
    private int tempScore;

    public void EnablePanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void DisablePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void SetScore(int score)
    {
        this.score = score;
        scoreText.text = this.score.ToString();
    }

    public void ResetScore()
    {
        totalScore -= tempScore;
    }

    public void SetFinalScore(int excessBricks)
    {
        bonusScore = excessBricks;
        tempScore = bonusScore + score;
        totalScore += tempScore;
        totalScoreText.text = totalScore.ToString();
        score = 0;
    }

    public void EnableScore()
    {
        scoreText.enabled = true;
    }

    public void DisableScore()
    {
        scoreText.enabled = false;
    }
}
