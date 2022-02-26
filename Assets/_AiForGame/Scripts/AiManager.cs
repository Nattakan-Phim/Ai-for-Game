using System;
using System.Collections;
using System.Collections.Generic;
using _AiForGame.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class AiManager : MonoBehaviour,IDamageable
{
    public event Action<int> OnTakeHit; 

    [SerializeField] private GameManager gameManager;
    [SerializeField] private WPManager wpManager;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float trackerSpeed;
    [SerializeField] private float distanceStop = 5;

    private Graph graph;
    private GameObject currentNode;
    private GameObject[] wps;

    private int currentWp = 0;
    private GameObject goal;
    private GameObject tracker;

    private void Start()
    {
        InstantiateTracker();
        SetGraphAndWp();
    }
    
    private void Update()
    {
        FindPath();
    }
    
    private void FindPath()
    {
        if (graph.getPathLength() == 0 || currentWp == graph.getPathLength())
        {
            return;
        }

        currentNode = graph.getPathPoint(currentWp);

        float distanceWp = Vector3.Distance(transform.position, currentNode.transform.position);

        if (distanceWp < 1)
        {
            currentWp++;
        }

        if (currentWp < graph.getPathLength())
        {
            goal = graph.getPathPoint(currentWp);

            ProgressTracker();
            PlayerMovement();
        }
    }

    private void SetGraphAndWp()
    {
        graph = wpManager.graph;
        wps = wpManager.waypoints;
        currentNode = wps[0];
    }

    private void ProgressTracker()
    {
        if (IsTrackerStop())
        {
            return;
        }

        TrackerLookWaypoints();
        TrackerMovement();
    }

    public void MoveTo(int indexWP)
    {
        graph.AStar(currentNode, wps[indexWP]);
        currentWp = 0;
    }

    private void PlayerMovement()
    {
        Quaternion lookAtTracker = Quaternion.LookRotation(tracker.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtTracker, rotationSpeed * Time.deltaTime);
        
        if (rotationSpeed < 3)
        {
            rotationSpeed = 5;
        }

        transform.Translate(0, 0, playerSpeed * Time.deltaTime);
    }

    private void InstantiateTracker()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cube);
        DestroyImmediate(tracker.GetComponent<Collider>());

        tracker.transform.position = transform.position;
        tracker.transform.rotation = transform.rotation;

        tracker.GetComponent<MeshRenderer>().enabled = false;
    }

    private void TrackerMovement()
    {
        tracker.transform.Translate(0, 0, trackerSpeed * Time.deltaTime);
    }

    private bool IsTrackerStop()
    {
        float trackerDistance = Vector3.Distance(transform.position, tracker.transform.position);
        return trackerDistance > distanceStop;
    }

    private void TrackerLookWaypoints()
    {
        tracker.transform.LookAt(goal.transform.position);
    }

    public void TakeHit(int count)
    {
        OnTakeHit?.Invoke(count);
    }
}