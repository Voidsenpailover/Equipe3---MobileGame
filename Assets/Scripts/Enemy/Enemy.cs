using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy 
{
    //public EnemyStat enemyStat;//Pas sur qu'il faut faire ça d'ou le commentaire
    private string Name {get ; set;}
    private string Type {get ; set;}
    private bool isAlive {get ; set;}
    public event Action OnDie; 
    private int Level {get ; set;}
    private SpriteRenderer Sprite {get ; set;}
    private float Speed {get ; set;}

    public Enemy(string name, string type, int level)
    {
        Name = name;
        Type = type;
        isAlive = true;
        Level = level;
    }
    
    public void Die()
    {
        isAlive = false;
        OnDie?.Invoke();    
    }
    
}
