using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class Pointter : MonoBehaviour
{

    private Random _Random = new Random();
    private int current = 0;
    [SerializeField] private GameObject[] Points;
    [SerializeField] private GameObject spawnedPoint;
    private PlayerScript player;
    

    private void Start()
    {
         RandomSpawned();
        // player = GetComponent<PlayerScript>();
    }
    
    public void RandomSpawned()
    {
        current = _Random.Next(Points.Length);
        Debug.Log($"current Node {current}");

        // create and set position
        Instantiate(spawnedPoint);
        spawnedPoint.transform.position = Points[current].transform.position;
        
        // var distancePlayer = Vector3.Distance(player.transform.position, Points[current].transform.position);
    }
    
    
    private void Update()
    {
        // player.FineNode(current);
        // Debug.Log($"current Node {current}");
        // var distancePlayer = Vector3.Distance(spawnedPoint.transform.position, player.transform.position);
        // if (distancePlayer < 1)
        // {
        //    RandomSpawned();
        // }
        
    }

    // private void SpawnPoint()
    // {
    //     spawnedPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //     spawnedPoint.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    //     transform.position = Points[current].transform.position;
    // }
    
}
