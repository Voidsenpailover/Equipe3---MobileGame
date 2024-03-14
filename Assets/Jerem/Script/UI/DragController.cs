using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public Draggable LastDragged => _lastDragged;

    private bool _isDragActive = false;
    private Vector2 _screenPosition;
    private Vector3 _worldPosition;
    private Draggable _lastDragged;

    public static event Action OnDropOnTurret;

    [SerializeField] private GridBuildingSystem _buildingSystem;
    [SerializeField] private GameObject _turretDragObject;


    private TurretsData _currentTurretData;

    private void OnEnable()
    {
        GridBuildingSystem.OnInfoMenuDragActive += WhenDragIsProc;
    }

    private void Awake()
    {
        DragController[] controller = FindObjectsOfType<DragController>();
        if(controller.Length > 1 )
        {
            Destroy(gameObject);
        }
        _lastDragged = _turretDragObject.GetComponent<Draggable>();
    }

    private void WhenDragIsProc(Vector3Int locOfGO)
    {
        _currentTurretData = GridBuildingSystem.TileDataBases[locOfGO].GetComponent<Building>().Data;
        _lastDragged.LastPosition = locOfGO;
        _lastDragged.LastTurret = GridBuildingSystem.TileDataBases[locOfGO];
    }

    void Update()
    {
        if (_isDragActive && (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            Drop();
            _buildingSystem.CanDrag = false;
            return;
        }

        if (_buildingSystem.CanDrag)
        {
            Vector3 mousePos = Input.mousePosition;
            _screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if(Input.touchCount > 0)
        {
            _screenPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }

        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);

        if(_isDragActive)
        {
            Drag();
        }
        else
        {
            /*RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
            if(hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                if(draggable != null)
                {
                    _lastDragged = draggable;
                    InitDrag();
                }
            }
            */
            if(_buildingSystem.IsDraggingNow)
            {
                _lastDragged = _turretDragObject.GetComponent<Draggable>();
                if(_lastDragged.transform.GetComponent<SpriteRenderer>() != null)
                {
                    _lastDragged.transform.GetComponent<SpriteRenderer>().sprite = _currentTurretData.Sprite;
                    _lastDragged.TurretData = _currentTurretData;
                }
                InitDrag();
            }
        }
    }

    void InitDrag()
    {
        _lastDragged.GetComponent<Draggable>().CanDrop = false;
        _lastDragged.GetComponent<Draggable>().DeactivateFusionUI();
        UpdateDragStatus(true);
    }

    void Drag()
    {
        _lastDragged.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);
    }

    void Drop()
    {
        UpdateDragStatus(false);
        _lastDragged.transform.position = new Vector2(100, 100);
        _buildingSystem.IsDraggingNow = false;
        _buildingSystem.CanSelect = true;
        OnDropOnTurret?.Invoke();
    }

    void UpdateDragStatus(bool IsDragging)
    {
        _isDragActive = _lastDragged.IsDragging = IsDragging;
        _lastDragged.gameObject.layer = IsDragging ? Layer.Dragging : Layer.Default;
    }
}
