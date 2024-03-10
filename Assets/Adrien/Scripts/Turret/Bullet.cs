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
    private int timingStun;
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
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, range);
    }
#endif

    private void Aoe()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, transform.forward, range, LayerMask.GetMask("Enemy"));
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

    private IEnumerator Stun(EnemyMovement enemy, float dureeStun)
    {
        enemy.MoveSpeed = 0;
        enemy.isStunned = true;
        yield return new WaitForSeconds(dureeStun + Turret.DelayBetweenAtk);
        enemy.MoveSpeed = enemy.EnemyStat.Speed;
        if (Turret.Type == TurretType.Fulgurite)
        {
            yield return new WaitForSeconds(dureeStun + 1f);
        }
        else
        {
            yield return new WaitForSeconds(dureeStun + 2f);
        }
        enemy.isStunned = false;
        Destroy(gameObject);
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
        Destroy(gameObject);
    }
    
    private IEnumerator Burn(EnemyMovement enemy)
    {
        enemy.isBurning = true;
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
        enemy.isBurning = false;
    }
    
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyMovement>())
        {
            var enemy = other.GetComponent<EnemyMovement>();
            switch (Turret.Type)
            {
                case TurretType.Feu:
                    if (enemy.EnemyStat.Vulnerability == Vulnerability.Feu) enemy.HP -= Turret.Damage * 2;
                    else enemy.HP -= Turret.Damage;
                    break;
                case TurretType.Eau:
                    if(enemy.EnemyStat.Vulnerability == Vulnerability.Eau) enemy.HP -= Turret.Damage * 2;
                    else enemy.HP -= Turret.Damage;
                    break;
                case TurretType.Terre:
                    if(enemy.EnemyStat.Vulnerability == Vulnerability.Terre) enemy.HP -= Turret.Damage * 2;
                    else enemy.HP -= Turret.Damage;
                    timingStun = Turret.Level;
                    break;
                case TurretType.Vent:
                    if(enemy.EnemyStat.Vulnerability == Vulnerability.Vent) enemy.HP -= Turret.Damage * 2;
                    else enemy.HP -= Turret.Damage;
                    break;
                case TurretType.Mercure:
                    switch (enemy.EnemyStat.Vulnerability)
                    {
                        case Vulnerability.Eau:
                            enemy.HP -= Turret.Damage * 2;
                            break;
                        case Vulnerability.Feu:
                            enemy.HP -= Turret.Damage * 0.5f;
                            break;
                        default:
                            enemy.HP -= Turret.Damage;
                            break;
                    }
                    break;
                case TurretType.Phosphore:
                    switch (enemy.EnemyStat.Vulnerability)
                    {
                        case Vulnerability.Feu:
                            enemy.HP -= Turret.Damage * 2;
                            break;
                        case Vulnerability.Terre:
                            enemy.HP -= Turret.Damage * 0.5f;
                            break;
                        default:
                            enemy.HP -= Turret.Damage;
                            break;
                    }
                    break;
                case TurretType.Fulgurite:
                    switch (enemy.EnemyStat.Vulnerability)
                    {
                        case Vulnerability.Terre:
                            enemy.HP -= Turret.Damage * 2;
                            break;
                        case Vulnerability.Vent:
                            enemy.HP -= Turret.Damage * 0.5f;
                            break;
                        default:
                            enemy.HP -= Turret.Damage;
                            break;
                    }
                    break;
                case TurretType.Pyrite:
                    switch (enemy.EnemyStat.Vulnerability)
                    {
                        case Vulnerability.Feu:
                            enemy.HP -= Turret.Damage * 2;
                            break;
                        case Vulnerability.Terre:
                            enemy.HP -= Turret.Damage * 0.5f;
                            break;
                        default:
                            enemy.HP -= Turret.Damage;
                            break;
                    }
                    break;
                default:
                    enemy.HP -= Turret.Damage;
                    break;
            }
            
            if (enemy.HP <= 0)
            {
                EnemySpawner._instance.EnemyReachedEndOfPath();
                Destroy(enemy.gameObject);
                Destroy(gameObject);
            }
            
            switch (Turret.AtkType)
            { 
                case TurretAtk.Stun:
                    if (!enemy.EnemyStat.CanBeStunned || enemy.isStunned){Destroy(gameObject);}
                    StartCoroutine(Stun(enemy, timingStun)); 
                    break;
                case TurretAtk.Explosion:
                    Aoe();
                    break;
                case TurretAtk.Slow: 
                    if(enemy.MoveSpeed < enemy.EnemyStat.Speed) {Destroy(gameObject);}
                    Slow(enemy);
                    break;
                case TurretAtk.Burn:
                    if(enemy.EnemyStat.Vulnerability == Vulnerability.Terre || enemy.isBurning) return;
                    StartCoroutine(Burn(enemy));
                    break;
            }
        }
    }
}
