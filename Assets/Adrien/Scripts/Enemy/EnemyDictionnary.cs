using System.Collections.Generic;
using UnityEngine;

   public class EnemyDictionnary : MonoBehaviour
   {
      private static Dictionary<EnemyTypes, EnemyStat> _enemy = new Dictionary<EnemyTypes, EnemyStat>();
      private static bool _initialized;
      private static void Initialize()
      {
         _enemy.Clear();
         Resources.LoadAll("Asset/ScriptableObject");
         if(Resources.FindObjectsOfTypeAll(typeof(EnemyStat)) is EnemyStat[] enemies)
         {
            foreach (var enemy in enemies)
            {
               _enemy.Add(enemy.Enemy, enemy);
            }
         }
         _initialized = true;
      }
   
   
      public static EnemyStat GetEnemyStat(EnemyTypes enemyType)
      {
         if (!_initialized)
         {
            Initialize();
         }
         return _enemy[enemyType];
      }
   }
