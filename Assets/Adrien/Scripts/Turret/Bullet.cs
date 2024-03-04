using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem.Interactions;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed = 5f;
    private Transform target;
    [SerializeField] private int range = 1;
    public TurretsData Turret;
    private float _timer;
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

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if(_timer > 3f)
        {
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, range);
    }

    private void Aoe()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, transform.forward, 0, 6);
        if(hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                 hit.transform.GetComponent<EnemyMovement>().HP -= 8;
                 if(hit.transform.GetComponent<EnemyMovement>().HP <= 0)
                 {
                     EnemySpawner._instance.EnemyReachedEndOfPath();
                     Destroy(hit.transform.gameObject);
                 }
            }
        }
        
    }

    private IEnumerator Stun(EnemyMovement enemy)
    {
        enemy.MoveSpeed = 0;
        yield return new WaitForSeconds(2f);
        enemy.MoveSpeed = enemy.EnemyStat.Speed;
    }

    private void Slow(EnemyMovement enemy)
    {
        
        switch (Turret.Level)
        {
            case 1:
                enemy.MoveSpeed *= 0.7f;
                break;
            case 2:
                enemy.MoveSpeed *= 0.5f;
                break;
            case 3:
                enemy.MoveSpeed *= 0.2f;
                break;
        }
    }
    
    private IEnumerator Burn(EnemyMovement enemy)
    {
        switch (Turret.Type)
        {
            case TurretType.Feu:
            switch (Turret.Level)
            {
                case 1:
                for(var i = 0; i < 3; i++)
                {
                    enemy.HP -= 1;
                    yield return new WaitForSeconds(1f);
                }
                break;
                case 2:
                    for(var i = 0; i < 3; i++)
                    {
                        enemy.HP -= 3;
                        yield return new WaitForSeconds(1f);
                    }
                    break;
                case 3:
                    for(var i = 0; i < 3; i++)
                    {
                        enemy.HP -= 10;
                        yield return new WaitForSeconds(1f);
                    }
                    break;
            }
            break;
            case TurretType.Pyrite:
                switch (Turret.Level)
                {
                    case 1:
                        for(var i = 0; i < 7; i++)
                        {
                            enemy.HP -= 1;
                            yield return new WaitForSeconds(1f);
                        }
                        break;
                    case 2:
                        for(var i = 0; i < 6; i++)
                        {
                            enemy.HP -= 3;
                            yield return new WaitForSeconds(1f);
                        }
                        break;
                }
                break;
        }
    }
    
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyMovement>())
        {
            var enemy = other.GetComponent<EnemyMovement>();
            enemy.HP -= Turret.Damage;
            if (enemy.HP <= 0)
            {
                EnemySpawner._instance.EnemyReachedEndOfPath();
                Destroy(enemy.gameObject);
            }
            switch (Turret.AtkType)
            { 
                case TurretAtk.Stun:
                    if (enemy.MoveSpeed == 0) return;
                    StartCoroutine(Stun(enemy)); 
                    break;
                case TurretAtk.Explosion:
                    Aoe();
                    break;
                case TurretAtk.Slow: 
                    if(enemy.MoveSpeed < enemy.EnemyStat.Speed) return;
                    Slow(enemy);
                    break;
                case TurretAtk.Burn:
                    StartCoroutine(Burn(enemy));
                    break;
            }

            if (other.GetComponent<EnemyMovement>().HP <= 0)
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
