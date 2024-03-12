using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

    public class Turret : MonoBehaviour
    {
        [SerializeField] private Transform gun;
        private float range = 4f;
        private Transform target;
        private float timeBetweenShots; 
        private float BulletPerSecond = 1f; 
        [SerializeField] private GameObject bulletPrefab;
        private List<EnemyMovement> enemies;
        private float turretDamage;
        private float localRange;
        private float localBPS;
        
        

        private TurretsData turret {get; set;}
        
        private void Update()
        {
            if (target == null)
            {
                FindTarget();
                return;
            }
            Rotate();
            
            if (!CheckIfInRange())
            {
                target = null;
            }
            else
            {
                timeBetweenShots += Time.deltaTime;
                if(timeBetweenShots >= 1f / BulletPerSecond)
                {
                    if (turret.Type == TurretType.Phosphore)
                    {
                        enemies = FindObjectsOfType<EnemyMovement>().ToList();
                        foreach (var enemy in enemies)
                        {
                            if (Vector2.Distance(enemy.transform.position, transform.position) <= range)
                            {
                                enemy.HP -= turret.Damage;
                            }
                        }
                        AudioManager.instance.PlaySound(AudioType.Phosphore, AudioSourceType.SFX);
                    }
                    else
                    {
                        FindTarget();
                        Shoot(turret);
                    }
                    timeBetweenShots = 0;
                }
            }
        }

        private void Rotate()
        {
            var angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            gun.rotation = rotation;
        }
        private void FindTarget()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, transform.forward, range, LayerMask.GetMask("Enemy"));
            if (hits.Length > 0)
            {
                target = hits[0].transform;
            }
            range = localRange;
            BulletPerSecond = localBPS;
            foreach (var card in UiManager.instance._listCard)
            {
                switch (card.CardName)
                {
                    case CardName.Verseau:
                        if (turret.Type == TurretType.Vent)
                        {
                            switch (card.Type)
                            {
                                case CardType.Soleil:
                                    range -= 1;
                                    BulletPerSecond *= 1.3f;
                                    break;
                                case CardType.Lune:
                                    range += 1;
                                    break;
                            }
                        
                        }break;
                    case CardName.Belier:
                        switch (turret.Level)
                        {
                            case 1:
                                switch (card.Type)
                                {
                                    case CardType.Soleil:
                                        BulletPerSecond *= 2;
                                        break;
                                    case CardType.Lune:
                                        
                                        break;
                                }
                                range += 1;
                            break;
                            case 2:
                                switch (card.Type)
                                {
                                    case CardType.Soleil:
                                        BulletPerSecond *= 0.5f;
                                        break;
                                    case CardType.Lune:
                                        break;
                                }
                                range -= 1;
                                break;
                        }
                        break;
                }
                
            }
        }
        
        private bool CheckIfInRange()
        {
            return Vector2.Distance(target.position, transform.position) <= range;
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
        Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, transform.forward, range);
#endif
    }

        private void Shoot(TurretsData _turret)
        {
            var bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Turret = _turret;
            bulletScript.localDamage = turretDamage;
            bulletScript.SetTarget(target);
            
            switch (turret.Type)
            {
                case TurretType.Eau:
                    AudioManager.instance.PlaySound(AudioType.Eau, AudioSourceType.SFX);
                    break;
                case TurretType.Feu:
                    AudioManager.instance.PlaySound(AudioType.Feu, AudioSourceType.SFX);
                    break;
                case TurretType.Vent:
                    AudioManager.instance.PlaySound(AudioType.Air, AudioSourceType.SFX);
                    break;
                case TurretType.Terre:
                    AudioManager.instance.PlaySound(AudioType.Terre, AudioSourceType.SFX);
                    break;
                case TurretType.Sel:
                    AudioManager.instance.PlaySound(AudioType.Sel, AudioSourceType.SFX);
                    break;
                case TurretType.Soufre:
                    AudioManager.instance.PlaySound(AudioType.Soufre, AudioSourceType.SFX);
                    break;
                case TurretType.Mercure:
                    AudioManager.instance.PlaySound(AudioType.Mercurehit, AudioSourceType.SFX);
                    break;
                case TurretType.Pyrite:
                    AudioManager.instance.PlaySound(AudioType.Pyrite, AudioSourceType.SFX);
                    break;
                case TurretType.Fulgurite:
                    AudioManager.instance.PlaySound(AudioType.fulgurite, AudioSourceType.SFX);
                    break;
            }
        }
        public void InitializeTurret(TurretsData data)
        {
            turret = data;
            turretDamage = data.Damage;
            localRange = data.RadAtk;
            localBPS = data.DelayBetweenAtk;
            range = localRange;
            BulletPerSecond = localBPS;
        }
        
    }