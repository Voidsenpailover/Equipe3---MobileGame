using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        private SpriteRenderer _spriteRenderer; 
        public List<RoundProperties> _rounds; 
        public int _currentRoundIndex;
        [SerializeField] private RoundProperties _curRound;
        [SerializeField] private float _timer;
        [SerializeField] private bool _isRoundOver;
        [SerializeField] private int _enemiesLeft;
        public static EnemySpawner _instance;
        public static event Action CardChoice;

        public static event Action OnWaveChanged;
        private void Awake()
        {
            _instance = this;
        }
        private void OnEnable()
        {
            LevelManager.OnGameStarted += StartRound;
        }

        private void OnDestroy() 
        {
            LevelManager.OnGameStarted -= StartRound;
        }

        private void Update()
        {
            if (!_isRoundOver) return;
        
            _timer += Time.deltaTime;
            if(_timer > 1f)
            {
                _timer = 0;
                if(_currentRoundIndex is 11 or 21 or 31)
                {
                    CardChoice?.Invoke();
                }
                OnWaveChanged?.Invoke();
                StartRound();  
                _isRoundOver = false;
            }
        }
        
        private void StartRound()
        {
            _currentRoundIndex++;
            
            if (_currentRoundIndex > _rounds.Count)
            {
                LevelManager.instance.Victory();
            }
            _curRound = _rounds[_currentRoundIndex - 1];
            _enemiesLeft = _curRound.EnemiesInRound;
            StartCoroutine(SpawnEnemiesInRound(_curRound));
        }

        private IEnumerator SpawnEnemiesInRound(RoundProperties roundProperties)
        {
            foreach (var spawnGroup in roundProperties.SpawnGroups)
            {
                yield return new WaitForSeconds(spawnGroup.InitialDelay);
                StartCoroutine(SpawnEnemiesInGroup(spawnGroup));
            }
        }

        private IEnumerator SpawnEnemiesInGroup(SpawnGroup spawnGroup)
        {
            var timeBetweenSpawns = new WaitForSeconds(spawnGroup.TimeBetweenSpawn);
            for (var i = 0; i < spawnGroup.NumberInGroup; i++)
            {
                SpawnEnemyType(spawnGroup.EnemyType);
                yield return timeBetweenSpawns;
            }
        }

        private void SpawnEnemyType(EnemyTypes enemyType)
        {
            var enemyProperties = EnemyDictionnary.GetEnemyStat(enemyType);
            SpawnEnemy(enemyProperties);
        }
    
        private void SpawnEnemy(EnemyStat enemyStat)
        {
            var prefab = Instantiate(enemyPrefab, LevelManager.instance.Chemin[0].transform.position, Quaternion.identity);
            var enemyMovement = prefab.GetComponent<EnemyMovement>();
            enemyMovement.InitializeEnemies(enemyStat);
        }
        
        
        public void EnemyReachedEndOfPath()
        {
            DecrementEnemiesLeftCount(1);
        }

        private void DecrementEnemiesLeftCount(int numberToDecrement)
        {
            _enemiesLeft -= numberToDecrement;
            if (_enemiesLeft <= 0)
            {
                _isRoundOver = true;
            }
        }
    }
    