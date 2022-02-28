using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AiManager aiManager;
    [SerializeField] private WPManager wpManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private Player player;
    [SerializeField] private GameObject egg;
    [SerializeField] private Button play;
    [SerializeField] private GameObject dialog;
    [SerializeField] private TextMeshProUGUI showWin;

    private Random _random = new Random();
    private int countEgg = 0;
    private bool isPlay = false;
    public int Point { get; private set; }

    private void Start()
    {
        dialog.gameObject.SetActive(true);
        player.OnTakeHit += OnPlayerTakeHit;
        aiManager.OnTakeHit += OnAITakeHit;
        play.onClick.AddListener(OnPlay);
        Time.timeScale = 0f;
    }

    public void Update()
    {
        if (isPlay == false)
        {
            dialog.gameObject.SetActive(true);
        }
        else
        {
            dialog.gameObject.SetActive(false);
        }
        
        if (countEgg != 0 || isPlay == false)
        {
            return;
        }

        Play();
    }

    private void Play()
    {
        SpawnEgg(_random.Next(wpManager.waypoints.Length));
    }

    private void OnPlay()
    {
        isPlay = true;
        Time.timeScale = 1f;
    }

    private void SpawnEgg(int point)
    {
        if (point == Point)
        {
            return;
        }

        Instantiate(egg, wpManager.waypoints[point].transform);
        countEgg++;
        Point = point;
        aiManager.MoveTo(point);
        ScoreWiener();
    }

    private void OnPlayerTakeHit(int count)
    {
        countEgg -= 1;
        scoreManager.PlayerScore += 1;
    }

    private void OnAITakeHit(int count)
    {
        countEgg -= 1;
        scoreManager.AIScore += 1;
    }

    private void ScoreWiener()
    {
        if (scoreManager.AIScore == 10 )
        {
            
            showWin.text = $" AI WINNER Score {scoreManager.AIScore}";
            StartCoroutine(WaitForMinute());
        }
        if (scoreManager.PlayerScore == 10)
        {
            
            showWin.text = $" PLAYER WINNER Score {scoreManager.PlayerScore} ";
            StartCoroutine(WaitForMinute());
        }
    }

    private IEnumerator WaitForMinute()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        Time.timeScale = 1f;
    }
}