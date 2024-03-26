using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool IsDragging;

    [SerializeField] GridBuildingSystem gridBuildingSystem;
    [SerializeField] FusionBehaviour fusionBehaviour;

    public static event Action<int> OnMoneyLoose;
    public static event Action<Vector3,TurretsData> OnFusionMenuActive;
    public static event Action OnFusionMenuDeactivated;

    private Vector3 _lastPosition;
    private GameObject _lastTurret;

    private BoxCollider2D _collider;
    private DragController _dragController;

    private float _movementTime = 15f;
    private System.Nullable<Vector3> _movementDestination;

    private TurretsData _turretData;
    private TurretsData newData;
    private bool _canDrop;
    private bool _isFusionUIStay = false;

    private Collider2D collider2dTemp;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GooglePlayManager googlePlayManager;

    public Vector3 LastPosition { get => _lastPosition; set => _lastPosition = value; }
    public TurretsData TurretData { get => _turretData; set => _turretData = value; }
    public bool CanDrop { get => _canDrop; set => _canDrop = value; }
    public GameObject LastTurret { get => _lastTurret; set => _lastTurret = value; }

    private void OnEnable()
    {
        DragController.OnDropOnTurret += DragController_OnDropOnTurret;

    }

    private void Start()
    {
        _collider = this.GetComponent<BoxCollider2D>();
        _dragController = FindObjectOfType<DragController>();
        CanDrop = false;   
    }

    private void DragController_OnDropOnTurret()
    {
        if (CanDrop)
        {
            _isFusionUIStay = true;
            CanDrop = false;
        }
    }

    public void MakeFusion()
    {
        newData = collider2dTemp.transform.gameObject.GetComponent<Building>().Data;
        var turret = collider2dTemp.GetComponent<Turret>();
        if (LastTurret.transform != collider2dTemp.transform)
        {
            if (TurretData.Level == 2 && collider2dTemp.transform.gameObject.GetComponent<Building>().Data.Level == 2)
            {
                newData = fusionBehaviour.SpawningRightTowerLevel3(TurretData, collider2dTemp.transform.gameObject.GetComponent<Building>().Data);
            }else if(TurretData.Level != collider2dTemp.transform.gameObject.GetComponent<Building>().Data.Level)
            {
                DeactivateFusionUI();
                turret.RemoveOutline();
                return;
            }
            else
            {
                newData = fusionBehaviour.SpawningRightTower(TurretData, collider2dTemp.transform.gameObject.GetComponent<Building>().Data);

            }
            int tempCost = levelManager.money - newData.Cost;
            if (tempCost >= 0)
            {
                levelManager.money = tempCost;
                collider2dTemp.transform.gameObject.GetComponent<Building>().Data = newData;
                collider2dTemp.transform.Find("Text").gameObject.GetComponent<SpriteRenderer>().sprite = newData.Sprite;
                _movementDestination = collider2dTemp.transform.position;
                gridBuildingSystem.ClearAreaDeuxPointZero(gridBuildingSystem.GridLayout.WorldToCell(LastPosition));
                GridBuildingSystem.TileDataBases.Remove(gridBuildingSystem.GridLayout.WorldToCell(LastPosition));
                Destroy(LastTurret);
                DeactivateFusionUI();
                turret.RemoveOutline();
                googlePlayManager.DoGrandAchievement(GPGSIds.achievement_apprentie);
            }
            else
            {
                DeactivateFusionUI();
                Debug.Log("Pas assez pour la fusion");
                turret.RemoveOutline();
            }

        }
        turret.InitializeTurret(newData);
    }

    public void DeactivateFusionUI()
    {
        OnFusionMenuDeactivated?.Invoke();
    }

    private void FixedUpdate()
    {
        if (_movementDestination.HasValue)
        {
            if( IsDragging )
            {
                _movementDestination = null;
                return;
            }

            if(transform.position != _movementDestination) 
            {
                gameObject.layer = Layer.Default;
                _movementDestination = null;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _movementDestination.Value, _movementTime * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(!other.CompareTag("Bullet") && other.excludeLayers == LayerMask.GetMask("FX"))
        {


            collider2dTemp = other;
            /*
            Draggable collidedDraggable = GetComponent<Draggable>();
            if (collidedDraggable != null && _dragController.LastDragged.gameObject == gameObject)
            {
                ColliderDistance2D colliderDistance2D = other.Distance(_collider);
                Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y, 0.0f) * colliderDistance2D.distance;
                transform.position -= diff;
            }
            */

            if (other.CompareTag("DropValid"))
            {
                newData = collider2dTemp.transform.gameObject.GetComponent<Building>().Data;
                if (LastTurret.transform != collider2dTemp.transform)
                {
                    newData = fusionBehaviour.SpawningRightTower(TurretData, collider2dTemp.transform.gameObject.GetComponent<Building>().Data);
                    if (TurretData.Level == 2 && collider2dTemp.transform.gameObject.GetComponent<Building>().Data.Level == 2)
                    {
                        newData = fusionBehaviour.SpawningRightTowerLevel3(TurretData, collider2dTemp.transform.gameObject.GetComponent<Building>().Data);
                    }
                    if (newData == gridBuildingSystem.TurretsData[24])
                    {
                        return;
                    }
                    else
                    {
                        var turret = other.GetComponent<Turret>();
                        Debug.Log(newData.name);
                        CanDrop = true;
                        OnFusionMenuActive?.Invoke(other.transform.position, newData);
                        turret.SetOutline();
                    }
                }
            } else if(other.CompareTag("DropInvalid"))
            {   
                _movementDestination = LastPosition;
                CanDrop = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (!collision.CompareTag("Bullet") && collision.excludeLayers == LayerMask.GetMask("FX"))
        {
            CanDrop = false;
            if (!_isFusionUIStay)
            {
                var turret = collision.GetComponent<Turret>();
                OnFusionMenuDeactivated?.Invoke();
                turret.RemoveOutline();
            }
        }
    }
}
