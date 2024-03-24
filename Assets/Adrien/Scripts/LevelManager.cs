using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public static event Action OnGameStarted;
    public static event Action OnGameOver;
    public static event Action OnVictory;
    public static event Action OnSell;
    public static event Action LowMoney;
    public static event Action HighMoney;

    public bool lvl;        
    
    public int HP = 200;
    public int money = 200;
    public int moneyToLoose;
    [SerializeField] private TextMeshProUGUI hpText;

    [SerializeField] GridBuildingSystem gridBuildingSystem;
    [SerializeField] GooglePlayManager googlePlayManager;

    [SerializeField] private float mult = 0.5f;

    public enum GameState
    {
        MainMenu,
        InGame,
        Victory,
        GameOver,
    }
    
    public GameState CurrentState;
    public Transform[] Chemin;

    private void OnEnable()
    {
        GridBuildingSystem.OnRoadEnd += GridBuildingSystem_OnRoadEnd;
        Draggable.OnMoneyLoose += WhenMoneyLoose;
        CardManager.CardSelected += Scorpion;
    }

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }
    
    private void Scorpion(CardData data)
    {
        if (data.CardName == CardName.Scorpion)
        {
            HP = 1;
            hpText.text = HP.ToString();
        }
    }
    
    private void WhenMoneyLoose(int cost)
    {
        money -= cost;
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
        if (lvl)
        {
            StartGame();
        }
    }
    
    public void StartGame()
    {
        CurrentState = GameState.InGame;
        OnGameStarted?.Invoke();
    }
    
    public void GameOver()
    {
        CurrentState = GameState.GameOver;
        Time.timeScale = 0;
        OnGameOver?.Invoke();
    }
        
    public void Victory()
    {
        CurrentState = GameState.Victory;
        Time.timeScale = 0;
        googlePlayManager.DoGrandAchievement(GPGSIds.achievement_alchimiste_confirm);
        OnVictory?.Invoke();
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LooseMoney()
    {
        money -= moneyToLoose;
    }
    public void LooseMoney(TurretsData data)
    {
        if((money -= data.Cost) > 0)
        {
            money -= data.Cost;
            gridBuildingSystem.HaveEnoughMoney = true;
        }
        else
        {
            gridBuildingSystem.HaveEnoughMoney = false;
        }
    }
    public void LooseMoneyForSell(TurretsData data)
    {
        Debug.Log("Test");
        money += (int)(data.Cost * mult);
        OnSell?.Invoke();
    }
}
