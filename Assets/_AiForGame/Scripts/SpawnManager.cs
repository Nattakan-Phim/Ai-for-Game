using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private AiManager aiManager;
    [SerializeField] private WPManager wpManager;
    [SerializeField] private GameObject egg;
    [SerializeField] private Button play;
    [SerializeField] private Text Text;

    private Random _random = new Random();
    private int countEgg = 0;
    private bool isPlay = false;
    public int Point { get; private set; }

    private void Start()
    {
        aiManager.OnTakeHit += OnTakeHit;
        play.onClick.AddListener(OnPlay);
    }

    public void Update()
    {
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
        play.GetComponent<CanvasRenderer>().Clear();
        Text.GetComponent<CanvasRenderer>().Clear();
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
    private void OnTakeHit(int count)
    {
        countEgg -= 1;
    }
}