using UnityEngine;

    public class DebugManager : MonoBehaviour
    { 
        [Header("Touches Debug")]
        [SerializeField] private KeyCode StartGame;

        [SerializeField] private GameObject _mainMenu;
        [SerializeField]private AudioSource _audioSource;
        [SerializeField] private AudioClip _musiqueDeFond;
        [SerializeField] private AudioClip _musiqueDeFond2;
    
    #if UNITY_EDITOR    
        private void Update()
        {
            if (Input.GetKeyDown(StartGame))
            {
                LevelManager.instance.StartGame();
            }
            
    #endif
            if(LevelManager.instance.CurrentState == LevelManager.GameState.MainMenu)
            {
                _audioSource.clip = _musiqueDeFond;
            }else if(LevelManager.instance.CurrentState == LevelManager.GameState.InGame)
            {
                _audioSource.clip = _musiqueDeFond2;
            }
        }
    public void LauncheGame()
        {
            LevelManager.instance.StartGame();
            _mainMenu.SetActive(false);
        }

    }
