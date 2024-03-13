using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        static public event Action OnGameStarted;
        static public event Action OnGameOver;
        static public event Action OnVictory;
    
        public int HP = 5;
        public int _money;
        public enum GameState
        {
            MainMenu,
            InGame,
            Victory,
            GameOver,
        }
    
        public GameState CurrentState;
        public Transform[] Chemin;
        
    
        private void Awake()
        {
            instance = this;
            Application.targetFrameRate = 60;
        }
    
        private void Start()
        {
            var parent = GameObject.Find("Chemin");
            Chemin = new Transform[parent.transform.childCount];
            for (var i = 0; i < parent.transform.childCount; i++)
            {
                Chemin[i] = parent.transform.GetChild(i);
            }
            CurrentState = GameState.MainMenu;
        }
    
        public void StartGame()
        {
            CurrentState = GameState.InGame;
            OnGameStarted?.Invoke();
        }
    
        public void GameOver()
        {
            CurrentState = GameState.GameOver;
            OnGameOver?.Invoke();
        }
    
        public void RestartGame()
        {
            SceneManager.LoadSceneAsync(1);
        }
        public void Victory()
        {
            CurrentState = GameState.Victory;
            OnVictory?.Invoke();
        }
    }
