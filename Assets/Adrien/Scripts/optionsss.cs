using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class optionsss : MonoBehaviour
{
  [SerializeField] private GameObject _options;
  
  [SerializeField] private GameObject _gameOverScene;
  [SerializeField] private GameObject _winScene;
  private void OnEnable()
  {
      LevelManager.OnGameOver += OnGameOver;
      LevelManager.OnVictory += OnWin;
  }

  private void OnDestroy()
  {
      LevelManager.OnGameOver -= OnGameOver;
      LevelManager.OnVictory -= OnWin;
  }

  private void OnWin()
  {
      _winScene.SetActive(true);
  }
  private void OnGameOver()
  {
      _gameOverScene.SetActive(true);
  }
    public void OpenOptions()
    {
        _options.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseOptions()
    {
        _options.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        _winScene.SetActive(false);
        _gameOverScene.SetActive(false);
        _options.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
        
    }
}
