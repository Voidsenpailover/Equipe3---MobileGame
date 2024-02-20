using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    public static LevelManager instance;

    
    public Transform[] Points;
    
    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        Points = GameObject.Find("Points").GetComponentsInChildren<Transform>();
        if(Points.Length == 1) {
            throw new NotImplementedException();
        }
    }
}
