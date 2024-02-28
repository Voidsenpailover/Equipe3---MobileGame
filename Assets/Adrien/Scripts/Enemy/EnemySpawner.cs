using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        private SpriteRenderer _spriteRenderer; 
        [SerializeField] private List<RoundProperties> _rounds;
        private RoundProperties _curRound;
        private int _currentRoundIndex = 0;
        private float timer;
        private bool _isRoundOver = false;
        private int _enemiesLeft;
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
        
            timer += Time.deltaTime;
            if(timer > 10f)
            {
                _isRoundOver = false;
                timer = 0;
                _currentRoundIndex++;
                if (_currentRoundIndex >= _rounds.Count)
                {
                    LevelManager.instance.GameOver();
                    return;
                }
                _curRound = _rounds[_currentRoundIndex];
                _enemiesLeft = _curRound.EnemyCount;
                StartRound();
            }
        }


        private void StartRound()
        {
            StartCoroutine(SpawnEnemiesInRound(_curRound));
        }
    
        private IEnumerator SpawnEnemiesInRound(RoundProperties roundProperties)
        {
            foreach (var spawnGroup in roundProperties.SpawnGroups)
            {
                yield return new WaitForSeconds(spawnGroup.InitialSpawnDelay);
                StartCoroutine(SpawnEnemiesInGroup(spawnGroup));
            }
        }

        private IEnumerator SpawnEnemiesInGroup(SpawnGroup spawnGroup)
        {
            WaitForSeconds timeBetweenSpawns = new WaitForSeconds(spawnGroup.TimeBetweenBloons);
            for (int i = 0; i < spawnGroup.NumberInGroup; i++)
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
    
        private EnemyMovement SpawnEnemy(EnemyStat enemyStat)
        {
            var prefab = Instantiate(enemyPrefab, LevelManager.instance.Points[0].transform.position, Quaternion.identity); 
            var enemyMovement = prefab.GetComponent<EnemyMovement>();
        
            enemyMovement.InitializeEnemies(enemyStat);
            return enemyMovement;
        }
    }

