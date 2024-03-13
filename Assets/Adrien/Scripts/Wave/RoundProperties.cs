using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Round-", menuName = "Scriptable Objects/RoundSO", order = 1)]
    public class RoundProperties : ScriptableObject
    {
        [SerializeField] private List<SpawnGroup> _spawnGroups;
        
        
        public int EnemiesInRound
        {
            get
            {
                var enemies = 0;
                foreach (var spawnGroup in _spawnGroups)
                {
                    enemies += spawnGroup.NumberInGroup;
                }
                return enemies;
            }
        }
        
        public List<SpawnGroup> SpawnGroups
        {
            get => _spawnGroups;
            set => _spawnGroups = value;
        }
    }
    

