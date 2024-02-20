using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    [Header("Spawn Settings")]
    [SerializeField] private int Enemies = 10;
    [SerializeField] private float enemiesPerSecond = 0.3f;
    private int currentWave = 1;
    private bool isSpawning = false;
    private int enemiesLeftToSpawn;
    private float timeBetweenSpawns;
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
        if(timeBetweenSpawns >= 1f / enemiesPerSecond)
        {
            timeBetweenSpawns = 0;
            if(enemiesLeftToSpawn > 0){
                SpawnEnemy();
                enemiesLeftToSpawn--;
            }else
            {
                isSpawning = false;
                EndWave();
            }
        }
    }
    private void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = Enemies;
    }
    
    private void EndWave()
    {
        currentWave++;
        Enemies += 5;
        enemiesPerSecond += 0.5f;
    }
    private void SpawnEnemy()
    {
       Instantiate(enemyPrefab, LevelManager.instance.Points[0].transform.position, Quaternion.identity);
    }
}
