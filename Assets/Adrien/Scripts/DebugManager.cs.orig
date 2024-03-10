using UnityEngine;

    public class DebugManager : MonoBehaviour
    { 
        [Header("Touches Debug")]
        [SerializeField] private KeyCode StartGame;
        [SerializeField] private KeyCode EndGame;
    
    #if UNITY_EDITOR    
        private void Update()
        {
            if (Input.GetKeyDown(StartGame))
            {
                LevelManager.instance.StartGame();
            }
        
            if (Input.GetKeyDown(EndGame))
            {
                LevelManager.instance.RestartGame();
            }
        }

        public void LauncheGame()
        {
            LevelManager.instance.StartGame();
        }
    #endif
    }
