using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class optionsss : MonoBehaviour
{
  [SerializeField] private GameObject _options;

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
        SceneManager.LoadScene(0);
    }
}
