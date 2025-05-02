using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cityGameManager : MonoBehaviour
{
    public static cityGameManager instance;
    
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public int maxLives = 3;
    
    private int score;
    private int lives;

    void Awake()
    {
        instance = this;
        lives = maxLives;
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = $"Score: {score}";
    }

    public void TakeDamage()
    {
        lives--;
        if (lives <= 0) EndGame();
    }

    void EndGame()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0;
    }
}