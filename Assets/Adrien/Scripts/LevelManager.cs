using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        public static event Action OnGameStarted;
        public static event Action OnGameOver;
        public static event Action OnVictory;
        
    
        public int HP = 5;
        public int money;
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
            GridBuildingSystem.OnRoadEnd += GridBuildingSystem_OnRoadEnd;
        }

        private void GridBuildingSystem_OnRoadEnd()
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
        
        public void Victory()
        {
            CurrentState = GameState.Victory;
            OnVictory?.Invoke();
        }

        public void LoadMainScene()
        {
            SceneManager.LoadScene(1);
        }
    }
