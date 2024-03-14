using System;
using UnityEngine;

    public class DebugManager : MonoBehaviour
    { 
        
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _tuto;

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
    
        
        public void CloseTuto()
        {
            _tuto.SetActive(false);
            Time.timeScale = 1;
        }
    }
