using UnityEngine;

[System.Serializable]
public struct Link
{
    public enum direction { Uni,BI }
    public GameObject node1;
    public GameObject node2;
    public direction Direction;
}

public class WPMenager : MonoBehaviour
{
    [SerializeField] private GameObject[] wayPoints;
    public Link[] links;
    public Graph Graph = new Graph();
    
    void Start()
    {
        if (wayPoints.Length <= 0) return;
        foreach (var wp in wayPoints)
        {
            Graph.AddNode(wp);
        }

        foreach (var l in links)
        {
            Graph.AddEdge(l.node1,l.node2);
            if (l.Direction == Link.direction.BI)
            {
                Graph.AddEdge(l.node2,l.node1);
            }
        }
    }
    
    void Update()
    {
        Graph.debugDraw();
    }
}
