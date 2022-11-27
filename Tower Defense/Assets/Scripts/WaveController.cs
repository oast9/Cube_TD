using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour
{
    [SerializeField] private float numberOfWaves;

    [SerializeField] private int timeBetweenWaves;
    [SerializeField] private float[] countOfEnemiesInCurrentWave;

    [SerializeField] private EnemyController[] enemies;
    [SerializeField] private float delayBetweenEnemyMoveStarts;

    private int currentWave;
    private int currentEnemyIndex;
    private int currentEnemyInWaveIndex;
    private int killedEnemiesInCurrentWave;

    private UIController _uiController;
    private int timeToNextWave;
    private void Awake()
    {
        _uiController = GameObject.Find("UIController").GetComponent<UIController>();
        timeToNextWave = timeBetweenWaves;
    }

    void Start()
    {
        currentWave = 0;
        killedEnemiesInCurrentWave = 0;
        StartCoroutine("SpawnTimer");
        StartCoroutine("UITimer");
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSecondsRealtime(timeBetweenWaves);
        SpawnEnemies();
    }

    IEnumerator UITimer()
    {
        yield return new WaitForSecondsRealtime(1);
        _uiController.UpdateTimer(timeToNextWave);
        timeToNextWave--;
        if (timeToNextWave < 0)
        {
            timeToNextWave = timeBetweenWaves;
            yield break;
        }
        StartCoroutine("UITimer");
    }
    
    void SpawnEnemies()
    {
        currentEnemyInWaveIndex = 0;
        killedEnemiesInCurrentWave = 0;
        StartCoroutine("PushEnemy");
    }

    IEnumerator PushEnemy()
    {
        yield return new WaitForSecondsRealtime(delayBetweenEnemyMoveStarts);
        enemies[currentEnemyIndex].StartMove();
        currentEnemyIndex++;
        currentEnemyInWaveIndex++;
        if (currentEnemyInWaveIndex < countOfEnemiesInCurrentWave[currentWave]) StartCoroutine("PushEnemy");
    }

    private void StartNextWave()
    {
        currentWave++;
        if (currentWave<numberOfWaves)
        {
            _uiController.IncreaseWaveCounter();
            StartCoroutine("SpawnTimer");
            StartCoroutine("UITimer");
        }
        else
        {
            LevelManager.LoadNextLevel();
        }
    }

    public void UpdateLocalKillCounter()
    {
        killedEnemiesInCurrentWave++;
        if (killedEnemiesInCurrentWave == countOfEnemiesInCurrentWave[currentWave])
            StartNextWave();
    }
}
