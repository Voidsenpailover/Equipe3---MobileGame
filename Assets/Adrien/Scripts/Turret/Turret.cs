using UnityEditor;
using UnityEngine;

    public class Turret : MonoBehaviour
    {
        [SerializeField] private Transform gun;
        [SerializeField] private float range = 4f;
        private Transform target;
        private float timeBetweenShots;
        [SerializeField] private float BulletPerSecond = 1f; 
        [SerializeField] private GameObject bulletPrefab;
        
       
        
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
                    Shoot();
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
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, transform.forward);
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
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, transform.forward, range);
        }

        private void Shoot()
        {
            var bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetTarget(target);
        }
    }