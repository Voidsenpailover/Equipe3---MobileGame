using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    [Header("Spawn Settings")]
    [Range(1,100)] public int currentWave = 1;
    [Range(0,1000)] public int Enemies = 10;
    public float enemiesPerSecond = 0.5f;
    public void Awake()
    {
        instance = this;
    }
}
