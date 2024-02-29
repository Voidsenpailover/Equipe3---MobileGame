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
        public Transform[] Points;
    
        private void Awake()
        {
            instance = this;
            Application.targetFrameRate = 60;
        }
    
        private void Start()
        {
            Points = GameObject.Find("Points").GetComponentsInChildren<Transform>();
            CurrentState = GameState.MainMenu;
        }
    
        public void StartGame()
        {
            CurrentState = GameState.InGame;
            OnGameStarted?.Invoke();
        }
    
        public void GameOver()
        {
            var tourFinale = Points.Length;
            Points[tourFinale - 1].GetComponent<SpriteRenderer>().color = Color.red;
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
