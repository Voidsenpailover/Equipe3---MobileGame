using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System;

  public class EnemyMovement : MonoBehaviour
  {
  
    [Header("Attributes")]
    private float MoveSpeed;
    private Rigidbody2D rb;
    private Transform target;
    private int Point;
    private float radius = 0.8f;
    private SpriteRenderer _spriteRenderer;
    private bool reachedEnd;
    private EnemyStat EnemyStat {get; set;}

    private void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      target = LevelManager.instance.Chemin[0];
    }

    private void Update()
    {
      if (!reachedEnd && Vector2.Distance(target.position, transform.position) <= 0.0001f)
      {
        Point++;
        if (Point >= LevelManager.instance.Chemin.Length)
        {
          reachedEnd = true;
          LevelManager.instance.HP -= EnemyStat.Damage;
          //AudioManager.instance.PlaySound(AudioType.Attaque, AudioSourceType.SFX);
          EnemySpawner._instance.EnemyReachedEndOfPath();
          Destroy(gameObject);
          if (LevelManager.instance.HP <= 0)
          {
            LevelManager.instance.GameOver();
          }
          return;
        }
        {
          target = LevelManager.instance.Chemin[Point];
        }
      }
    }

    private void FixedUpdate()
    {
      Vector2 direction = (target.position - transform.position).normalized;
    
      rb.velocity = direction * MoveSpeed;
    }
  
    private void OnDrawGizmos()
    {
      Handles.color = Color.red;
      Handles.DrawWireDisc(transform.position, transform.forward, radius);
    }
  
    public void InitializeEnemies(EnemyStat enemyStat)
    {
      EnemyStat = enemyStat;
      MoveSpeed = enemyStat.Speed;
    }

  }

