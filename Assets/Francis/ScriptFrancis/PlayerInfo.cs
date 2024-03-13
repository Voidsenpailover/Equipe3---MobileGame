using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshPro t1;
    TextMeshPro t2;
    TextMeshPro t3;
    GameObject pausePanel;
    int life = 10;
    int wave = 1;
    int waveMax = 30;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
