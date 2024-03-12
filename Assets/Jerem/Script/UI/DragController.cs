using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public Draggable LastDragged => _lastDragged;

    private bool _isDragActive = false;
    private Vector2 _screenPosition;
    private Vector3 _worldPosition;
    private Draggable _lastDragged;

    [SerializeField] private GridBuildingSystem _buildingSystem;
    [SerializeField] private GameObject _turretDragObject;

    private void Awake()
    {
        DragController[] controller = FindObjectsOfType<DragController>();
        if(controller.Length > 1 )
        {
            Destroy(gameObject);
        }
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
                InitDrag();
            }
        }
    }

    void InitDrag()
    {
        _lastDragged.LastPosition = _lastDragged.transform.position;
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
        Debug.Log("Wtf le drop");
    }

    void UpdateDragStatus(bool IsDragging)
    {
        _isDragActive = _lastDragged.IsDragging = IsDragging;
        _lastDragged.gameObject.layer = IsDragging ? Layer.Dragging : Layer.Default;
    }
}
