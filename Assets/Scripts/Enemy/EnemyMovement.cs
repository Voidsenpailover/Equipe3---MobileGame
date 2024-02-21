using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyMovement : MonoBehaviour
{
  
  [Header("Attributes")]
  [SerializeField] private float MoveSpeed = 2f;
  
  
  
  private Rigidbody2D rb;
  private Transform target;
  private int Point = 0;
  private float radius = 0.8f;


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
}
