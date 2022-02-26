using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int PlayerScore { get; set; }
    public int AIScore { get; set; }

    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI aiScoreText;

    private void Start()
    {
        PlayerScore = 0;
        AIScore = 0;
        SetScorePlayer(PlayerScore.ToString());
        SetScoreAI(AIScore.ToString());
    }

    private void Update()
    {
        SetScorePlayer(PlayerScore.ToString());
        SetScoreAI(AIScore.ToString());

        if (PlayerScore == 10 || AIScore == 10)
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
    }


    private void SetScorePlayer(string playerScore)
    {
        playerScoreText.text = $"Player Point: {playerScore}";
    }

    private void SetScoreAI(string aiScore)
    {
        aiScoreText.text = $"Player Point: {aiScore}";
    }
}