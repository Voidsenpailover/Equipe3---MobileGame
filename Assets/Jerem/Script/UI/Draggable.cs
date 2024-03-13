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

    private Collider2D collider2dTemp;

    public Vector3 LastPosition { get => _lastPosition; set => _lastPosition = value; }
    public TurretsData TurretData { get => _turretData; set => _turretData = value; }
    public bool CanDrop { get => _canDrop; set => _canDrop = value; }

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
            Debug.Log("Il a drop");
            collider2dTemp.transform.gameObject.GetComponent<Building>().Data = TurretData;
            collider2dTemp.transform.gameObject.GetComponent<SpriteRenderer>().sprite = TurretData.Sprite;
            _movementDestination = collider2dTemp.transform.position;
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
            Debug.Log("Hey");

        } else if(other.CompareTag("DropInvalid"))
        {
            _movementDestination = LastPosition;
            Debug.Log(_movementDestination);
            CanDrop = false;
        }
        
    }
}
