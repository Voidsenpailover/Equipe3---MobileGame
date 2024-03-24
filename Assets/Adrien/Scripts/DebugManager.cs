using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{ 
        
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _tuto;
    [SerializeField] private GameObject _creditsScene;
    [SerializeField] private GameObject _selectionMenu;

    private void Start()
    {
        AudioManager.instance.PlaySound(AudioType.World, AudioSourceType.Music);
    }

    public void LauncheGame()
    {
        _selectionMenu.SetActive(false);
        _tuto.SetActive(true);
        Time.timeScale = 0;
    }
        
    public void LaunchSpecialGame(int id)
    {
        SceneManager.LoadScene(id);
    }
    
    public void OpenCredits()
    {
        _creditsScene.SetActive(true);
    }
    public void OpenLevelSelection()
    {
        _mainMenu.SetActive(false);
        _selectionMenu.SetActive(true);
    }
    public void CloseLevelSelection()
    {
        _mainMenu.SetActive(true);
        _selectionMenu.SetActive(false);
    }
    public void CloseCredits()
    {
        _creditsScene.SetActive(false);
    }
    public void CloseTuto()
    {
        _tuto.SetActive(false);
        LevelManager.instance.StartGame();
        Time.timeScale = 1;
        AudioManager.instance.PlaySound(AudioType.Music, AudioSourceType.Music);
    }
}
