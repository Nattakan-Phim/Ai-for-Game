using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Link
{
    public enum Directions
    {
        UNI,
        BI
    }

    public GameObject node1;
    public GameObject node2;

    public Directions dir;
}

public class WPManager : MonoBehaviour
{
    public GameObject[] waypoints;
    public Graph graph = new Graph();
    
    [SerializeField] private Link[] links;
    
    private void Start()
    {
        InstantiateGraph();
    }

    private void InstantiateGraph()
    {
        if (waypoints.Length > 0)
        {
            foreach (var wp in waypoints)
            {
                graph.AddNode(wp);
            }

            foreach (var l in links)
            {
                graph.AddEdge(l.node1, l.node2);

                if (l.dir == Link.Directions.BI)
                {
                    graph.AddEdge(l.node2, l.node1);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        graph.debugDraw();
    }
}