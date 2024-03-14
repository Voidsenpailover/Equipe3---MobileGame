using UnityEngine;

    public class DebugManager : MonoBehaviour
    { 
        [Header("Touches Debug")]
        [SerializeField] private KeyCode StartGame;

        [SerializeField] private GameObject _mainMenu;
    
    #if UNITY_EDITOR    
        private void Update()
        {
            if (Input.GetKeyDown(StartGame))
            {
                LevelManager.instance.StartGame();
            }
            
        }
    #endif
    public void LauncheGame()
        {
            LevelManager.instance.StartGame();
            _mainMenu.SetActive(false);
        }

    }
