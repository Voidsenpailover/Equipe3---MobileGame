using UnityEngine;
    
[System.Serializable]
    public class SpawnGroup
    {
        [SerializeField] private float _initialSpawnDelay;
        [SerializeField] private EnemyTypes _enemyType;
        [SerializeField] private int _numberInGroup;
        [SerializeField] private float _timeBetweenSpawn;
        public float InitialDelay
        {
            get => _initialSpawnDelay;
            set => _initialSpawnDelay = value;
        }
    
        public EnemyTypes EnemyType
        {
            get => _enemyType;
            set => _enemyType = value;
        }
    
        public int NumberInGroup
        {
            get => _numberInGroup;
            set => _numberInGroup = value;
        }
    
        public float TimeBetweenSpawn
        {
            get => _timeBetweenSpawn;
            set => _timeBetweenSpawn = value;
        }
        
    }

