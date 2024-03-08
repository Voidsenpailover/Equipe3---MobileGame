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
    [SerializeField] private CapsuleCollider2D collider;
    [SerializeField] private float bulletSpeed = 5f;
    private Transform target;
    [Header("Taille d'explosion")]
    [SerializeField] private int range = 1;
    public TurretsData Turret;
    private float _timer;
    private int timingStun;
    private int timingBurn = 3;
    private int burnDamage;
    private float slowPercent;
    
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
                    switch (Turret.Level)
                    {
                        case 1:
                            burnDamage = 1;
                            break;
                        case 2:
                            burnDamage = 3;
                            break;
                        case 3:
                            burnDamage = 10;
                            break;
                    }
                    break;
                case TurretType.Eau:
                    if(enemy.EnemyStat.Vulnerability == Vulnerability.Eau) enemy.HP -= Turret.Damage * 2;
                    else enemy.HP -= Turret.Damage;
                    
                    switch(Turret.Level)
                    {
                        case 1:
                            slowPercent = 0.2f;
                            break;
                        case 2:
                            slowPercent = 0.5f;
                            break;
                        case 3:
                            slowPercent = 0.7f;
                            break;
                    }
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
                    timingStun = Turret.Level + 1;
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

                    switch (Turret.Level)
                    {
                        case 1:
                            burnDamage = 1;
                            break;
                        case 2:
                            burnDamage = 3;
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
                LevelManager.instance.money += enemy.EnemyStat.Money;
                Destroy(enemy.gameObject);
                Destroy(gameObject);
            }
            
            switch (Turret.AtkType)
            { 
                case TurretAtk.Stun:
                    if (!enemy.EnemyStat.CanBeStunned){Destroy(gameObject);}
                    enemy.ApplyStun(timingStun);
                    break;
                case TurretAtk.Explosion:
                    Aoe();
                    break;  
                case TurretAtk.Slow: 
                    enemy.ApplySlow(3, slowPercent);
                    break;
                case TurretAtk.Burn:
                    if(enemy.EnemyStat.Vulnerability == Vulnerability.Terre) return;
                    enemy.ApplyBurn(timingBurn, burnDamage);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
