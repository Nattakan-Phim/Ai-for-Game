using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class Pointter : MonoBehaviour
{

    private Random _Random = new Random();
    private int current;
    [SerializeField] private GameObject[] Points;
    private GameObject spawnedPoint;
    private PlayerScript player;
    
    void Start()
    {
        Random();
    }
    
    
    private void Random()
    {
        current = _Random.Next(Points.Length);
        SpawnPoint();
        player.FineNode(current);
    }

    private void SpawnPoint()
    {
        spawnedPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        spawnedPoint.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
    
}
