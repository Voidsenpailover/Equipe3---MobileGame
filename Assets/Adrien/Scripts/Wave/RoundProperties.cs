using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Round-", menuName = "Scriptable Objects/RoundSO", order = 1)]
    public class RoundProperties : ScriptableObject
    {
        [SerializeField] private List<SpawnGroup> _spawnGroups;

        public List<SpawnGroup> SpawnGroups
        {
            get => _spawnGroups;
            set => _spawnGroups = value;
        }
        
        public float RoundTime
        {
            get
            {
                var maxGroupTime = 0f;
                foreach (var spawnGroup in _spawnGroups)
                {
                    var groupTime = spawnGroup.InitialDelay + spawnGroup.NumberInGroup * spawnGroup.TimeBetweenSpawn;
                    maxGroupTime = Mathf.Max(maxGroupTime, groupTime);
                }
                return maxGroupTime;
            }
        }
    
        
    }
    

