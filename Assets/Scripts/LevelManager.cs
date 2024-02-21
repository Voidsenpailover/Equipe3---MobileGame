using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    static public event Action OnGameStarted;
    static public event Action OnGameOver;
    
    public int HP = 5;
    public enum GameState
    {
        MainMenu,
        InGame,
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
        int tourFinale = Points.Length;
        Points[tourFinale - 1].GetComponent<SpriteRenderer>().color = Color.red;
        CurrentState = GameState.GameOver;
        OnGameOver?.Invoke();
    }
    
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
