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
        private List<EnemyMovement> enemies;
        private float turretDamage;
        private float localRange;
        private float localBPS;
        public int compteurTour;

        private TurretsData turret {get; set;}

        private Material _material;

        private void Start()
        {  
            _material = this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().material;
        }

        public void SellTower()
        {
            compteurTour--;
            Destroy(gameObject);
        }
        
        
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
                        Instantiate(turret.PrefabBullet, transform.transform.position, Quaternion.identity);
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
                                    if(range >= 2) range -= 1;
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
                                }
                                range += 1;
                            break;
                            case 2:
                                switch (card.Type)
                                {
                                    case CardType.Soleil:
                                        BulletPerSecond *= 0.5f;
                                        break;
                                }
                                if(range >= 2) range -= 1;
                                break;
                        }
                        break;
                    case CardName.Taureau:
                        if (range >= 2)
                        {
                            range -= 1;
                        }
                        break;
                    case CardName.Cancer:
                        if(card.Type == CardType.Lune) BulletPerSecond *= 0.5f;
                        break;
                    case CardName.Sagittaire:
                        range += 2;
                        break;
                    case CardName.Capricorne:
                        switch (card.Type)
                        {
                            case CardType.Soleil:
                               switch (turret.Type)
                               {
                                   case TurretType.Terre:
                                       range += 2;
                                       break;
                                   case TurretType.Vent:
                                       BulletPerSecond *= 0.9f;
                                       break;
                               }
                               break;
                        }break;
                    case CardName.Poisson:
                            switch (card.Type)
                            {
                                case CardType.Soleil:
                                switch (turret.Type)
                                {
                                    case TurretType.Eau:
                                        BulletPerSecond *= 1.5f;
                                        break;
                                    case TurretType.Vent:
                                        range += 1;
                                        break;
                                }
                                break;
                                case CardType.Lune:
                                if (turret.Type == TurretType.Terre)
                                {
                                    BulletPerSecond *= 0.75f;
                                }
                                break;
                            }
                        break;
                    case CardName.Scorpion:
                        switch (card.Type)
                        {
                            case CardType.Soleil:
                                BulletPerSecond *= 2f;
                                break;
                            case CardType.Lune:
                                range += 3;
                                break;
                        }
                        break;
                    case CardName.Vierge:
                        if (turret.Level == 3)
                        {
                            if(card.Type == CardType.Soleil){
                                    BulletPerSecond *= 2f;
                            }
                        }
                        else
                        {
                            if(card.Type == CardType.Soleil){
                                BulletPerSecond *= 0.25f;
                            }
                        }
                        break;
                    case CardName.Balance:
                        if (compteurTour <= 4)
                        {
                            switch (card.Type)
                            {
                                case CardType.Soleil:
                                    BulletPerSecond *= 1.5f;
                                    range += 1;
                                    break;
                            }
                        }break;
                    case CardName.Gémeaux:
                        if (turret.Type is TurretType.Eau or TurretType.Vent or TurretType.Feu or TurretType.Terre)
                        {
                            switch (card.Type)
                            {
                                case CardType.Soleil:
                                    range -= 1;
                                    break;
                                case CardType.Lune:
                                    range -= 1;
                                    break;
                            }
                        }else
                        {
                            switch (card.Type)
                            {
                                case CardType.Soleil:
                                    range += 1;
                                    break;
                                case CardType.Lune:
                                    range += 1;
                                    break;
                            }
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
            if (turret.Type == TurretType.Fulgurite)
            {
                var lighting = Instantiate(turret.PrefabBullet, target.position + new Vector3(0, 2), Quaternion.identity);
                var bulletScript = lighting.GetComponent<Bullet>();
                bulletScript.Turret = _turret;
                bulletScript.localDamage = turretDamage;
                bulletScript.SetTarget(target);
            }
            else
            {
                var bullet = Instantiate(turret.PrefabBullet, gun.transform.position, Quaternion.identity);
                var bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.Turret = _turret;
                bulletScript.localDamage = turretDamage;
                bulletScript.SetTarget(target);
                bulletScript.compteurTurret = compteurTour;
            }
            
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
            compteurTour++;
            
        }

         public void SetOutline()
        {
            _material.SetColor("_OutlineColor", Color.white);   
        }

        public void RemoveOutline()
        {
            _material.SetColor("_OutlineColor", Color.black);   
        }
    }