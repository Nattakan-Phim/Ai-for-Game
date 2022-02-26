using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}