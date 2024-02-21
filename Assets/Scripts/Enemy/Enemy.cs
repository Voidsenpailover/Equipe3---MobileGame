using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy 
{
    private string Name {get ; set;}
    private string Type {get ; set;}
    private bool isAlive {get ; set;}
    public event Action OnDie; 
    private int Level {get ; set;}
    private SpriteRenderer Sprite {get ; set;}
    
    public Enemy(string name, string type, bool isAlive, SpriteRenderer sprite)
    {
        Name = name;
        Type = type;
        isAlive = true;
        Sprite = sprite;
    }
    
    public void Die()
    {
        isAlive = false;
        OnDie?.Invoke();    
    }
    
}
