using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class Waypoints : MonoBehaviour

{
    public Transform[] waypoints;
    public int numberOfPoints;
    public int curDes;
    [SerializeField]
    private float minDist;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        numberOfPoints = waypoints.Length;
        agent.autoBraking = false;
        minDist = 3f;
        curDes = -1;
        GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (curDes == numberOfPoints - 1)
        {
            return;
        }
        else
        {
            curDes++;
            agent.destination = waypoints[curDes].position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(transform.position, waypoints[curDes].position) < minDist)
        {
            GoToNextPoint();
        }
    }
}