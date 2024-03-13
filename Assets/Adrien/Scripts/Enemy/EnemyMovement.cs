using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System;

  public class EnemyMovement : MonoBehaviour
  {
  
    [Header("Attributes")]
    public float MoveSpeed;
    private Rigidbody2D rb;
    private Transform target;
    [SerializeField] private int Point;
    private float radius = 0.8f;
    private SpriteRenderer _spriteRenderer;
    private bool reachedEnd;
    public float HP;
    public bool isStunned = false;
    public bool isBurning = false;
    public EnemyStat EnemyStat {get; private set;}

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
        }else
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
#if UNITY_EDITOR

        Handles.color = Color.red;
      Handles.DrawWireDisc(transform.position, transform.forward, radius);
#endif
    }
  
    public void InitializeEnemies(EnemyStat enemyStat)
    {
      EnemyStat = enemyStat;
      MoveSpeed = enemyStat.Speed;
      HP = enemyStat.Hits;
    }

  }

