using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

  public class EnemyMovement : MonoBehaviour
  {
  
    [Header("Attributes")]
    private float MoveSpeed;
    private Rigidbody2D rb;
    private Transform target;
    private int Point = 0;
    private float radius = 0.8f;
    private EnemyStat _enemyStat;
    private SpriteRenderer _spriteRenderer;
    private int hitsRemaining;
  
  
    public EnemyStat EnemyStat
    {
      get => _enemyStat;
      set => _enemyStat = value;
    }

    private void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      target = LevelManager.instance.Points[Point];
    }

    private void Update()
    {
      if (Vector2.Distance(target.position, transform.position) <= 0.1f){
        Point++;
        if (Point >= LevelManager.instance.Points.Length){
          LevelManager.instance.HP -= 1;
          AudioManager.instance.PlaySound(AudioType.Attaque, AudioSourceType.SFX);
          Destroy(gameObject);
          if (LevelManager.instance.HP <= 0)
          {
            LevelManager.instance.GameOver();
          }
          return;
        }else {
          target = LevelManager.instance.Points[Point];
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
      _enemyStat = enemyStat;
      hitsRemaining = enemyStat.Hits;
      MoveSpeed = enemyStat.Speed;
    }
  }

