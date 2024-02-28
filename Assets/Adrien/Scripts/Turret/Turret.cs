using UnityEditor;
using UnityEngine;

    public class Turret : MonoBehaviour
    {
        [SerializeField] private Transform gun;
        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private float range = 4f;
        [SerializeField, Range(250, 1000)] private float speedRotation = 250f;
        private Transform target;

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
        }

        private void Rotate()
        {
            float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            gun.rotation = Quaternion.RotateTowards(gun.transform.rotation, rotation, speedRotation * Time.deltaTime);
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
    }