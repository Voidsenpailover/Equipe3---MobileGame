using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool IsDragging;

    [SerializeField] GridBuildingSystem gridBuildingSystem;
    [SerializeField] FusionBehaviour fusionBehaviour;

    public static event Action<int> OnMoneyLoose;
    public static event Action<Vector3> OnFusionMenuActive;
    public static event Action OnFusionMenuDeactivated;

    private Vector3 _lastPosition;
    private GameObject _lastTurret;

    private BoxCollider2D _collider;
    private DragController _dragController;

    private float _movementTime = 15f;
    private System.Nullable<Vector3> _movementDestination;

    private TurretsData _turretData;
    private bool _canDrop;

    private Collider2D collider2dTemp;

    public Vector3 LastPosition { get => _lastPosition; set => _lastPosition = value; }
    public TurretsData TurretData { get => _turretData; set => _turretData = value; }
    public bool CanDrop { get => _canDrop; set => _canDrop = value; }
    public GameObject LastTurret { get => _lastTurret; set => _lastTurret = value; }

    private void Start()
    {
        _collider = this.GetComponent<BoxCollider2D>();
        _dragController = FindObjectOfType<DragController>();
        DragController.OnDropOnTurret += DragController_OnDropOnTurret;
        CanDrop = false;   
    }

    private void DragController_OnDropOnTurret()
    {
        if (CanDrop)
        {
            TurretsData newData = collider2dTemp.transform.gameObject.GetComponent<Building>().Data;
            if (TurretData.Type != collider2dTemp.transform.gameObject.GetComponent<Building>().Data.Type)
            {
                newData = fusionBehaviour.SpawningRightTower(TurretData, collider2dTemp.transform.gameObject.GetComponent<Building>().Data);
                collider2dTemp.transform.gameObject.GetComponent<Building>().Data = newData;
                collider2dTemp.transform.Find("Text").gameObject.GetComponent<SpriteRenderer>().sprite = newData.Sprite;
                _movementDestination = collider2dTemp.transform.position;
                gridBuildingSystem.ClearAreaDeuxPointZero(gridBuildingSystem.GridLayout.WorldToCell(LastPosition));
                GridBuildingSystem.TileDataBases.Remove(gridBuildingSystem.GridLayout.WorldToCell(LastPosition));
                OnMoneyLoose?.Invoke(newData.Cost);
                Destroy(LastTurret);
            }
            CanDrop = false;
        }
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
            CanDrop = true;
            OnFusionMenuActive?.Invoke(other.transform.position);
        } else if(other.CompareTag("DropInvalid"))
        {
            _movementDestination = LastPosition;
            CanDrop = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CanDrop = false;
        OnFusionMenuDeactivated?.Invoke();
    }
}
