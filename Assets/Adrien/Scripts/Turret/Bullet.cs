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
    [Header("Taille d'explosion")]
    [SerializeField] private int range = 1;
    public TurretsData Turret;
    private float _timer;
    private float timingStun = 1;
    private float timingBurn = 3;
    private float burnDamage = 1;
    private float slowPercent;
    private int mercureMoney;
    public float localDamage;
    private float turretDamage;
    public int compteurTurret;

    public static event Action OnDamage;


   

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
            }
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyMovement>())
        {
            var enemy = other.GetComponent<EnemyMovement>();
            
            if (enemy.isDebuffed)
            {
                turretDamage = localDamage;
                turretDamage *= enemy.debuffPercent;
            }else {
                turretDamage = localDamage;
            }
            
            foreach (var card in UiManager.instance._listCard)
            {
                switch (card.CardName)
                {
                    case CardName.Verseau:
                    if (card.Type == CardType.Lune && Turret.Type == TurretType.Feu)
                    {
                        turretDamage *= 0.5f;
                    }
                    break;
                    case CardName.Belier:
                        if (card.Type == CardType.Soleil)
                        {
                            switch (Turret.Level)
                            {
                                case 1:
                                    turretDamage *= 2f;
                                    break;
                                case 2:
                                case 3:    
                                    turretDamage *= 0.75f;
                                    break;
                            }
                        }else if (card.Type == CardType.Lune)
                        {
                            switch (Turret.Level)
                            {
                                case 1:
                                    slowPercent *= 2;
                                    timingStun *= 2;
                                    break;
                                case 2:
                                case 3:    
                                    slowPercent *= 0.75f;
                                    timingStun *= 0.5f;
                                    break;
                            }
                        }
                        break;
                    case CardName.Taureau:
                        switch (card.Type)
                        {
                            case CardType.Soleil:
                                turretDamage *= 2f;
                                break;
                            case CardType.Lune:
                                break;
                        }
                        break;
                    case CardName.Lion:
                        switch (card.Type)
                        {
                            case CardType.Soleil:
                                burnDamage *= 1.5f;
                                slowPercent *= 0.5f;
                                break;
                            case CardType.Lune:
                                burnDamage *= 2f;
                                timingStun *= 0.5f;
                                break;
                        }
                        break;
                    case CardName.Cancer:
                        switch (card.Type)
                        {
                            case CardType.Soleil:
                                slowPercent *= 2f;
                                burnDamage *= 2f;
                                turretDamage *= 0.5f;
                                break;
                            case CardType.Lune:
                                timingBurn *= 2;
                                timingStun *= 2f;
                                break;
                        }
                        break;
                    case CardName.Sagittaire:
                        switch (card.Type)
                        {
                            case CardType.Soleil:
                                turretDamage *= 0.7f;
                                break;
                            case CardType.Lune:
                                slowPercent *= 0.7f;
                                burnDamage *= 0.7f;
                                break;
                        }break;
                    case CardName.Capricorne:
                        if (card.Type == CardType.Lune)
                        {
                            if (Turret.Type == TurretType.Terre)
                            {
                                timingStun *= 2f;
                            }
                            burnDamage *= 0.5f;
                        }break;
                    case CardName.Poisson:
                        if (card.Type == CardType.Lune)
                        {
                            slowPercent *= 1.1f;
                        }
                        break;
                    case CardName.Balance:
                        if (compteurTurret <= 4)
                        {
                            switch (card.Type)
                            {
                                case CardType.Soleil:
                                    turretDamage *= 1.5f;
                                    break;
                                case CardType.Lune:
                                    slowPercent *= 1.5f;
                                    burnDamage *= 1.5f;
                                    timingStun *= 1.5f;
                                    timingBurn *= 1.5f;
                                    break;
                            }
                        }
                        break;
                    case CardName.Vierge:
                        if(Turret.Level == 3)
                        {
                            switch (card.Type)
                            {
                                case CardType.Soleil:
                                    turretDamage *= 2f;
                                    break;
                                case CardType.Lune:
                                    slowPercent *= 1.3f;
                                    burnDamage *= 1.3f;
                                    timingStun *= 2f;
                                    timingBurn *= 2f;
                                    break;
                            }
                        }
                        else
                        {
                            switch (card.Type)
                            {
                                case CardType.Soleil:
                                    turretDamage *= 0.25f;
                                    break;
                                case CardType.Lune:
                                    slowPercent *= 0.5f;
                                    burnDamage *= 0.5f;
                                    timingStun *= 0.25f;
                                    timingBurn *= 0.25f;
                                    break;
                            }
                        }
                        break;
                    case CardName.Gémeaux:
                        if (Turret.Type is TurretType.Eau or TurretType.Vent or TurretType.Feu or TurretType.Terre)
                        {
                            switch (card.Type)
                            {
                                case CardType.Soleil:
                                    turretDamage *= 0.5f;
                                    break;
                                case CardType.Lune:
                                    turretDamage *= 0.5f;
                                    break;
                            }
                        }else
                        {
                            switch (card.Type)
                            {
                                case CardType.Soleil:
                                    turretDamage *= 1.5f;
                                    break;
                                case CardType.Lune:
                                    turretDamage *= 1.5f;
                                    break;
                            }
                        }
                        break;
                }
                
            }
            
            switch (Turret.Type)
            {
                case TurretType.Feu:
                    if (enemy.EnemyStat.Vulnerability == Vulnerability.Feu) enemy.HP -= turretDamage * 1.5f;
                    else enemy.HP -= turretDamage;
                    switch (Turret.Level)
                    {
                        case 2:
                            burnDamage += 3;
                            break;
                        case 3:
                            burnDamage += 10;
                            break;
                    }
                    break;
                case TurretType.Eau:
                    if(enemy.EnemyStat.Vulnerability == Vulnerability.Eau) enemy.HP -= turretDamage * 1.5f;
                    else enemy.HP -= turretDamage;
                    
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
                    if(enemy.EnemyStat.Vulnerability == Vulnerability.Terre) enemy.HP -= turretDamage * 1.5f;
                    else enemy.HP -= turretDamage;
                    timingStun *= Turret.Level;
                    break;
                case TurretType.Vent:
                    if(enemy.EnemyStat.Vulnerability == Vulnerability.Vent) enemy.HP -= turretDamage * 1.5f;
                    else enemy.HP -= turretDamage;
                    break;
                case TurretType.Mercure:
                    switch (enemy.EnemyStat.Vulnerability)
                    {
                        case Vulnerability.Eau:
                            enemy.HP -= turretDamage * 1.5f;
                            break;
                        case Vulnerability.Feu:
                            enemy.HP -= turretDamage * 0.5f;
                            break;
                        default:
                            enemy.HP -= turretDamage;
                            break;
                    }

                    switch (Turret.Level)
                    {
                        case 1:
                            mercureMoney += 1;
                            break;
                        case 2:
                            mercureMoney += 3;
                            break;
                    }
                    enemy.ApplyMercure(mercureMoney);
                    break;
                case TurretType.Fulgurite:
                    switch (enemy.EnemyStat.Vulnerability)
                    {
                        case Vulnerability.Terre:
                            enemy.HP -= turretDamage * 1.5f;
                            break;
                        case Vulnerability.Vent:
                            enemy.HP -= turretDamage * 0.5f;
                            break;
                        default:
                            enemy.HP -= turretDamage;
                            break;
                    }
                    timingStun *= Turret.Level + 1;
                    break;
                case TurretType.Pyrite:
                    switch (enemy.EnemyStat.Vulnerability)
                    {
                        case Vulnerability.Feu:
                            enemy.HP -= turretDamage * 1.5f;
                            break;
                        case Vulnerability.Terre:
                            enemy.HP -= turretDamage * 0.5f;
                            break;
                        default:
                            enemy.HP -= turretDamage;
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
                case TurretType.Sel:
                    switch (Turret.Level)
                    {
                        case 1:
                            enemy.debuffPercent = 1.5f;
                            break;
                        case 2:
                            enemy.debuffPercent = 2f;
                            break;
                    }
                    enemy.HP -= turretDamage;
                    enemy.ApplyDebuff(3);
                    break;
                default:
                    enemy.HP -= turretDamage;
                    break;
            }
            OnDamage?.Invoke();

            enemy.CallDamageFlash();
            
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
