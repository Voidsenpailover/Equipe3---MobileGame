using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        private SpriteRenderer _spriteRenderer; 
        [SerializeField] private List<RoundProperties> _rounds;
        private RoundProperties _curRound;
        private int _currentRoundIndex => LevelManager.instance._round - 1;
        private float _timer;
        private bool _isRoundOver;
        private int _enemiesLeft;
        public static EnemySpawner instance;

        private void Awake()
        {
            instance = this;
        }
        private void Start()
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
            if(_timer > 10f)
            {
                _timer = 0;
                _isRoundOver = false;
                StartRound();
            }
        }

   
        private void StartRound()
        {
            LevelManager.instance._round++;
            if (_currentRoundIndex >= _rounds.Count)
            {
                LevelManager.instance.Victory();
                return;
            }
            _curRound = _rounds[_currentRoundIndex];
            _enemiesLeft = 0;
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
                _enemiesLeft++;
                yield return timeBetweenSpawns;
            }
        }

        private void SpawnEnemyType(EnemyTypes enemyType)
        {
            var enemyProperties = EnemyDictionnary.GetEnemyStat(enemyType);
            SpawnEnemy(enemyProperties);
        }
    
        private EnemyMovement SpawnEnemy(EnemyStat enemyStat)
        {
            var prefab = Instantiate(enemyPrefab, LevelManager.instance.Points[0].transform.position, Quaternion.identity); 
            var enemyMovement = prefab.GetComponent<EnemyMovement>();
        
            enemyMovement.InitializeEnemies(enemyStat);
            return enemyMovement;
        }
        
        private void EnemyDead(EnemyStat enemyStat)
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

