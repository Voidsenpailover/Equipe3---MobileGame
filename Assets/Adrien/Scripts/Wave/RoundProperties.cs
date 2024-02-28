using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Round-", menuName = "Scriptable Objects/RoundSO", order = 1)]
    public class RoundProperties : ScriptableObject
    {
        [SerializeField] private List<SpawnGroup> _spawnGroups;
        private int _roundNumber;
    
        public List<SpawnGroup> SpawnGroups
        {
            get => _spawnGroups;
            set => _spawnGroups = value;
        }
    
        public int RoundNumber
        {
            get => _roundNumber;
            set => _roundNumber = Mathf.Max(1, value);
        }
    
        public float RoundTime
        {
            get
            {
                var maxGroupTime = 0f;
                foreach (var spawnGroup in _spawnGroups)
                {
                    var groupTime = spawnGroup.InitialSpawnDelay + spawnGroup.NumberInGroup * spawnGroup.TimeBetweenBloons;
                    maxGroupTime = Mathf.Max(maxGroupTime, groupTime);
                }
    
                return maxGroupTime;
            }
        }
    
        public int EnemyCount
        {
            get
            {
                int totalEnemiesCount = 0;
                foreach (var spawnGroup in _spawnGroups)
                {
                    totalEnemiesCount += spawnGroup.NumberInGroup;
                }
                return totalEnemiesCount;
            }
        }
    }
    

