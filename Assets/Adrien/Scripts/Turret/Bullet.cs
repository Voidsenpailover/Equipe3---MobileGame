using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed = 5f;
    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
  
    private void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyMovement>())
        {
            other.GetComponent<EnemyMovement>().HP--;
           
            Debug.Log(other.GetComponent<EnemyMovement>().HP);
            if(other.GetComponent<EnemyMovement>().HP <= 0)
            {
                Debug.Log("Enemy hit" + other.GetComponent<EnemyMovement>().HP);
                EnemySpawner._instance.EnemyReachedEndOfPath();
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
