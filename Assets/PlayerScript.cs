using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerScript : MonoBehaviour
{
    // [SerializeField] private CharacterController PlayerCon;
    [SerializeField] private int speedPlayer;
    [SerializeField] private int speedRote;
    [SerializeField] private int overDistance;
    [SerializeField] private GameObject[] wayPoint;
    [SerializeField] private int speedTracker;
    private int cerrentWP = 0;
    private GameObject goal;
    private GameObject tracker;
    private GameObject cerrentNode;
    [SerializeField] private WPManager wpMenager;
    private Graph m_Graph;
    private Pointter point;
    private GameObject spawnedPoint;



    void Start()
    {
       // PlayerCon = GetComponent<CharacterController>();
       point = GetComponent<Pointter>();
       InstantiateTracker();
       m_Graph = wpMenager.graph;
       cerrentNode = wayPoint[0];
    }

    void Update()
    {
        PlayerMoveTo();
        TrackerLookWP();
        NodeProgress();
    }

    private void InstantiateTracker()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cube);
        DestroyImmediate(tracker.GetComponent<Collider>()); 
        DestroyImmediate(tracker.GetComponent<MeshRenderer>());
        tracker.transform.localScale = new Vector3(0f, 0, 0);
        

        tracker.transform.position = transform.position;
        tracker.transform.rotation = transform.rotation;
    }
    

    private void NodeProgress()
    {
        if (m_Graph.getPathLength() == 0 || cerrentWP == m_Graph.getPathLength())
        {
            return;
        }
        cerrentNode = m_Graph.getPathPoint(cerrentWP);
        var disrectionWP = Vector3.Distance(transform.position, cerrentNode.transform.position);
        if (disrectionWP < 1f)
        {
            cerrentWP++;
        }

        if (cerrentWP >= m_Graph.getPathLength()) { return; }
        goal = m_Graph.getPathPoint(cerrentWP);
        
    }
    
    private void TrackerLookWP()
    {
        tracker.transform.LookAt(cerrentNode.transform);
        tracker.transform.Translate(0,0,speedTracker*Time.deltaTime);
    }

    private void PlayerMoveTo()
    {
        if (speedRote < 5)
        {
            speedRote = 5;
        }
        var trackerDistance = Vector3.Distance(transform.position, tracker.transform.position);
        if (trackerDistance < overDistance || trackerDistance < 1f)
        {
            return;
        }
        // var lookGoal = Quaternion.LookRotation(tracker.transform.position - transform.position);
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookGoal, speedRote*Time.deltaTime);
        transform.LookAt(tracker.transform);
        transform.Translate(0,0,speedPlayer*Time.deltaTime);
    }

    // fine node want
    public void FineNode(int index)
    {
        m_Graph.AStar(cerrentNode, wayPoint[index]);
        cerrentWP++;
    }
    
}
