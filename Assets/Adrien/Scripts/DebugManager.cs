using System;
using UnityEngine;

    public class DebugManager : MonoBehaviour
    { 
        
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _tuto;
         [SerializeField] private GameObject _creditsScene;

        private void Start()
        {
            AudioManager.instance.PlaySound(AudioType.World, AudioSourceType.Music);
        }

        public void LauncheGame()
        {
            LevelManager.instance.StartGame();
            _mainMenu.SetActive(false);
            _tuto.SetActive(true);
            Time.timeScale = 0;
        }
    
        public void OpenCredits()
        {
            _creditsScene.SetActive(true);
        }
        public void CloseCredits()
        {
            _creditsScene.SetActive(false);
        }
        public void CloseTuto()
        {
            _tuto.SetActive(false);
            AudioManager.instance.PlaySound(AudioType.Music, AudioSourceType.Music);
            Time.timeScale = 1;
        }
    }
