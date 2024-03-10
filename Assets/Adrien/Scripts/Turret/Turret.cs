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
            var bullet = Instantiate(turret.AtkSFX, gun.transform.position, Quaternion.identity);
            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Turret = _turret;
            bulletScript.SetTarget(target);
        }
        public void InitializeTurret(TurretsData data)
        {
            turret = data;
            range = data.RadAtk;
            BulletPerSecond = data.DelayBetweenAtk;
        }
        
    }