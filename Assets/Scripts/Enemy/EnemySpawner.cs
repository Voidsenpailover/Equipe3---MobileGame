using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private float timeBetweenSpawns;
    private WaveManager waveManager;

    private void Start()
    {
        LevelManager.OnGameStarted += StartWave;
    }

    private void OnDestroy()
    {
        LevelManager.OnGameStarted -= StartWave;
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeBetweenSpawns += Time.deltaTime;

        if (timeBetweenSpawns >= 1f / WaveManager.instance.enemiesPerSecond + WaveManager.instance.currentWave * 0.1f)
        {
            timeBetweenSpawns = 0;
            if (enemiesLeftToSpawn > 0)
            {
                SpawnEnemy();
                enemiesLeftToSpawn--;
            }
            else
            {
                isSpawning = false;
                EndWave();
               StartCoroutine(RestartRound());
            }
        }
    }
    
    public void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = WaveManager.instance.Enemies;
    }

    private void EndWave()
    {
        WaveManager.instance.enemiesPerSecond += 0.1f;
        WaveManager.instance.Enemies += 5;
        WaveManager.instance.currentWave++;
    }
    
    IEnumerator RestartRound()
    {
        yield return new WaitForSeconds(10f);
        StartWave();
    }

    private void SpawnEnemy()
    {
        var prefab = Instantiate(enemyPrefab, LevelManager.instance.Points[0].transform.position, Quaternion.identity); 
    }
}
