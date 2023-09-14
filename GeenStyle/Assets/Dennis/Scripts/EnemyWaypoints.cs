using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypoints : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex;
    private float enemySpeed = 5f;

    private void Update()
    {
        EnemyWaypointTravel();
    }

    public void EnemyWaypointTravel()
    {
        Transform wp = waypoints[currentWaypointIndex];
        if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        {
            currentWaypointIndex = currentWaypointIndex + 1 % waypoints.Length;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, wp.position, enemySpeed * Time.deltaTime);
            transform.LookAt(transform.position);
        }

        if (currentWaypointIndex >= waypoints.Length)
        {
            Destroy(gameObject);
            return;
        }
    }
}
