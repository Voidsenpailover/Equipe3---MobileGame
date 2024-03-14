using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    

    public BoundsInt area;
    [SerializeField] private TurretsData _data;

    //Properties
    public TurretsData Data { get => _data; set => _data = value; }

    void Start()
    {
        this.transform.Find("Text").GetComponent<SpriteRenderer>().sprite = Data.Sprite;
    }

    #region Build Methods

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuildingSystem.current.GridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (GridBuildingSystem.current.CanTakeArea(areaTemp))
        {
            return true;
        }
        return false;  
    }

    public void Place()
    {
        Vector3Int positionInt = GridBuildingSystem.current.GridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        GridBuildingSystem.current.TakeArea(area);
    }

    #endregion
}
