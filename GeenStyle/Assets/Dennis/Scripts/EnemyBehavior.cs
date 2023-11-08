using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour

{
    [SerializeField]
    private Transform healthCanvas;

    [Header("Auto-Fill")]
    public GameObject waypointParent;
    public Transform[] targets;

    [Header("Attributes")]
    public int numberOfPoints;
    public int curDes;
    public float startHealth;
    public float health;
    public int worthAmount;
    public Image healthBar;
    public AudioSource deathSound;

    [SerializeField]
    private float minDist;
    private NavMeshAgent agent;
    private WaveSpawner waveSpawner;
    private bool isDead = false;

    [Header("Animations")]
    public Animator animator;

    #region Awake & Update
    void Awake()
    {
        health = startHealth;
        waypointParent = GameObject.Find("Waypoints");
        agent = GetComponent<NavMeshAgent>();
        numberOfPoints = waypointParent.transform.childCount;
        agent.autoBraking = false;
        minDist = 0.25f;
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

    private void LateUpdate()
    {
        healthCanvas.LookAt(transform.position + Camera.main.transform.forward);
    }
    #endregion

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            isDead = true;

            if (isDead)
            {
                gameObject.tag = "DeadEnemy";

                PlayerStats.money += worthAmount;
                animator = GetComponent<Animator>();
                animator.SetTrigger("Death");

                GetComponent<NavMeshAgent>().speed = 0;
                GetComponent<NavMeshAgent>().acceleration = 100000;
                StartCoroutine(EnemyDead());
            }
        }
    }
    public IEnumerator EnemyDead()
    {
        deathSound.Play();
        waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    #region Next Waypoint
    void GoToNextPoint()
    {
        if (curDes == numberOfPoints - 1)
        {
            waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;
            PlayerStats.lives -= 1;
            Destroy(gameObject);
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
