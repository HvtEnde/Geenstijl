using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] 
    private float countdown;
    [SerializeField] 
    private GameObject spawnPoint;
    [SerializeField] 
    GameObject winScreen;

    public Wave[] waves;

    public GameManager gameManager;

    public int currentWaveIndex = 0;

    private bool readytoCountdown;

    #region Start and Update
    private void Start()
    {
        readytoCountdown = true;

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
    }


    void Update()
    {
        if (currentWaveIndex >= waves.Length)
        {
            winScreen.SetActive(true);
            gameManager.WinLevel();
            return;
        }

        if (readytoCountdown == true)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0)
        {
            readytoCountdown = false;

            countdown = waves[currentWaveIndex].timeToNextWave;
            StartCoroutine(SpawnWave());
        }

        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            readytoCountdown = true;
            currentWaveIndex++;
        }
    }
    #endregion

    #region SpawnWave
    private IEnumerator SpawnWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
            {
                EnemyBehavior enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.transform);

                enemy.transform.SetParent(spawnPoint.transform);

                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }
}
#endregion

[System.Serializable]

public class Wave
{
    public EnemyBehavior[] enemies;
    public float timeToNextEnemy;
    public float timeToNextWave;

    [HideInInspector] public int enemiesLeft;
}