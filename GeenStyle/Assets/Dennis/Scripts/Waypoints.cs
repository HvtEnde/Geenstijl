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
    private WaveSpawner waveSpawner;
    public GameObject[] waypointArray;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        numberOfPoints = waypoints.Length;
        agent.autoBraking = false;
        minDist = 3f;
        curDes = -1;
        GoToNextPoint();
        waveSpawner = GetComponentInParent<WaveSpawner>();
    }

    void GoToNextPoint()
    {
        if (curDes == numberOfPoints - 1)
        {
            Destroy(gameObject);
            waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;
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
