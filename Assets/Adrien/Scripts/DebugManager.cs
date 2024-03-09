using UnityEngine;

    public class DebugManager : MonoBehaviour
    { 
        [Header("Touches Debug")]
        [SerializeField] private KeyCode StartGame;
    
    #if UNITY_EDITOR    
        private void Update()
        {
            if (Input.GetKeyDown(StartGame))
            {
                LevelManager.instance.StartGame();
            }
            
        }

        public void LauncheGame()
        {
            LevelManager.instance.StartGame();
        }
    #endif
    }
