using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool IsDragging;

    [SerializeField] GridBuildingSystem gridBuildingSystem;

    private Vector3 _lastPosition;

    private BoxCollider2D _collider;
    private DragController _dragController;

    private float _movementTime = 15f;
    private System.Nullable<Vector3> _movementDestination;

    private TurretsData _turretData;
    private bool _canDrop;

    public Vector3 LastPosition { get => _lastPosition; set => _lastPosition = value; }
    public TurretsData TurretData { get => _turretData; set => _turretData = value; }

    private void Start()
    {
        _collider = this.GetComponent<BoxCollider2D>();
        _dragController = FindObjectOfType<DragController>();
        DragController.OnDropOnTurret += DragController_OnDropOnTurret;
        _canDrop = false;   
    }

    private void DragController_OnDropOnTurret()
    {
        _canDrop = true;
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
        Draggable collidedDraggable = GetComponent<Draggable>();
        if (collidedDraggable != null && _dragController.LastDragged.gameObject == gameObject)
        {
            ColliderDistance2D colliderDistance2D = other.Distance(_collider);
            Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y, 0.0f) * colliderDistance2D.distance;
            transform.position -= diff;
        }

        if (other.CompareTag("DropValid"))
        {
            if(_canDrop)
            {
                _movementDestination = other.transform.position;
                Debug.Log("Il a drop");
                other.transform.gameObject.GetComponent<Building>().Data = TurretData;
                other.transform.gameObject.GetComponent<SpriteRenderer>().sprite = TurretData.Sprite;
                _canDrop = false;
            }
        } else if(other.CompareTag("DropInvalid"))
        {
            _movementDestination = LastPosition;
            Debug.Log(_movementDestination);
        }
    }
}
