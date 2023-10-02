using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour

{
    public GameObject waypointParent;
    public Transform[] targets;
    public int numberOfPoints;
    public int curDes;
    [SerializeField]
    private float minDist;
    private NavMeshAgent agent;
    private WaveSpawner waveSpawner;

    #region Awake & Update
    void Awake()
    {
        waypointParent = GameObject.Find("Waypoints");
        agent = GetComponent<NavMeshAgent>();
        numberOfPoints = waypointParent.transform.childCount;
        agent.autoBraking = false;
        minDist = 3f;
        curDes = -1;
        for(int i = 0; i < waypointParent.transform.childCount; i++)
        {
            targets[i] = waypointParent.transform.GetChild(i).transform;
        }
        waveSpawner = GetComponentInParent<WaveSpawner>();
        GoToNextPoint();
    }


    void Update()
    {
        CheckDistance();
    }
    #endregion

    #region Next Waypoint
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
    #endregion

    #region Distance Check
    void CheckDistance()
    {
        if(Vector3.Distance(transform.position, targets [curDes].position) < minDist)
        {
            GoToNextPoint();
        }
    }
    #endregion
}