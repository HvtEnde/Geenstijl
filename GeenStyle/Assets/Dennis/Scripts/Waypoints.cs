using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Waypoints : MonoBehaviour

{
    public GameObject waypointParent;
    public Transform[] targets;
    public int numberOfPoints;
    public int curDes;
    [SerializeField]
    private float minDist;
    private NavMeshAgent agent;
    private WaveSpawner waveSpawner;

    // Start is called before the first frame update
    void Awake()
    {
        waypointParent = GameObject.Find("Waypoints");
        agent = GetComponent<NavMeshAgent>();
        numberOfPoints = waypointParent.transform.childCount;
        agent.autoBraking = false;
        minDist = 3f;
        curDes = 0;
        for(int i = 0; i < waypointParent.transform.childCount; i++)
        {
            targets[i] = waypointParent.transform.GetChild(i).transform;
        }
        waveSpawner = GetComponentInParent<WaveSpawner>();
        GoToNextPoint();
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
            agent.destination = targets[curDes].position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(transform.position, targets [curDes].position) < minDist)
        {
            GoToNextPoint();
        }
    }
}
