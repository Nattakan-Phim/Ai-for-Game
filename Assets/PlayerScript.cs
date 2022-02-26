using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private WPMenager wpMenager;
    private Graph m_Graph;
    
    void Start()
    {
       // PlayerCon = GetComponent<CharacterController>();
       InstantiateTracker();
       m_Graph = wpMenager.Graph;
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
        // tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        

        tracker.transform.position = transform.position;
        tracker.transform.rotation = transform.rotation;
    }
    

    private void NodeProgress()
    {
        if (m_Graph.getPathLength() == 0 )
        {
            return;
        }
        cerrentNode = m_Graph.getPathPoint(cerrentWP);
        var disrectionWP = Vector3.Distance(transform.position, cerrentNode.transform.position);
        if (disrectionWP == 1)
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
        if (trackerDistance < overDistance || trackerDistance < 1)
        {
            return;
        }
        // var lookGoal = Quaternion.LookRotation(tracker.transform.position - transform.position);
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookGoal, speedRote*Time.deltaTime);
        transform.LookAt(tracker.transform);
        transform.Translate(0,0,speedPlayer*Time.deltaTime);
    }

    // fine node want
    public void FineNode(int indexWP)
    {
        m_Graph.AStar(cerrentNode, wayPoint[indexWP]);
        cerrentWP++;
    }
    
}
