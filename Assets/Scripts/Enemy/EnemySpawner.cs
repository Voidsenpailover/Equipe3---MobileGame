using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy();
        }
        #endif

        if (!isSpawning) return;

        timeBetweenSpawns += Time.deltaTime;

        if (timeBetweenSpawns >= 1f / WaveManager.instance.enemiesPerSecond)
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
            }
        }
    }
    
    private void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = WaveManager.instance.Enemies;
        WaveManager.instance.enemiesPerSecond += 0.1f;
    }

    private void EndWave()
    {
        WaveManager.instance.Enemies += 5;
        WaveManager.instance.currentWave++;
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, LevelManager.instance.Points[0].transform.position, Quaternion.identity);
    }
}
