using UnityEngine;
    
[System.Serializable]
    public class SpawnGroup
    {
        [SerializeField] private float _initialSpawnDelay;
        [SerializeField] private EnemyTypes _enemyType;
        [SerializeField] private int _numberInGroup;
        [SerializeField] private float _timeBetweenBloons;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public float InitialSpawnDelay
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
    
        public float TimeBetweenBloons
        {
            get => _timeBetweenBloons;
            set => _timeBetweenBloons = value;
        }
    
        public SpriteRenderer Sprite
        {
            get => _spriteRenderer;
            set => _spriteRenderer = value;
        }
    }

