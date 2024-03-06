using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class GridBuildingSystem : MonoBehaviour
{
    //Instanciate
    [SerializeField] public static GridBuildingSystem current;

    //Events
    public static event Action<Vector3> OnTurretMenuActive;
    public static event Action OnTurretMenuDeactivated;

    //TileMaps
    [SerializeField] private GridLayout gridLayout;
    [SerializeField] Tilemap mainTilemap;
    [SerializeField] Tilemap tempTilemap;

    //TileBase
    private static List<TileBase> _tiles;
    private static Dictionary<TileType, List<TileBase>> tileBases = new Dictionary<TileType, List<TileBase>>();
    [SerializeField] private RessourceTileDataBase _sourceTileData;

    //TilesPos
    private Building temp;
    private Vector3Int prevPos;
    private BoundsInt prevArea;

    //Turrets Data
    private int _currentID;
    [SerializeField] private List<TurretsData> _turretsData;

    //Properties
    public GridLayout GridLayout { get => gridLayout; set => gridLayout = value; }
    public int CurrentID { get => _currentID; set => _currentID = value; }

    //Selections
    private bool _canSelect;

    #region Unity Methods

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        tileBases.Add(TileType.Empty, _sourceTileData.EmptyTile);
        tileBases.Add(TileType.Green, _sourceTileData.SelectionTile);
        tileBases.Add(TileType.White, _sourceTileData.FloorTile);
        tileBases.Add(TileType.Road, _sourceTileData.RoadTile);
        //tileBases.Add(TileType.Red, Resources.Load<TileBase>(_tilePath + "SquareR"));
        _canSelect = true;
        
    }

    private void Update()
    {
        /// Clicking System 
        /// Input ON TOUCH (to change) 
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) * 1 / GridLayout.transform.localScale.x; //where the point is
            Vector3Int cellPos = GridLayout.LocalToCell(touchPos); //corresponding touch to his cell

            TileBase tileSelected = mainTilemap.GetTile(cellPos); //Tile Selected

            if (tileSelected == tileBases[TileType.Empty][0] || tileSelected == tileBases[TileType.Road][0]) //If it's beyond grid
            {
                return;
            }
            if (_canSelect) // Check For selection
            {
                _canSelect = false; //Un-allow Spaming
                ClearArea(); //Clear Tiles for TEMP
                prevPos = cellPos; //Keep Pos For Further utility

                //Selection TILE
                TileBase tileTex = tileBases[TileType.Green][0]; 
                tempTilemap.SetTile(cellPos, tileTex);
                
                //Event for UI
                OnTurretMenuActive?.Invoke(GridLayout.CellToLocalInterpolated(cellPos + new Vector3(.5f, .5f, 0f)));
            }
            else //UI already UP
            {
                //Check for UP/DOWN/LEFT/RIGHT Pos For Buttons
                if(cellPos != prevPos && cellPos != new Vector3Int(prevPos.x +1, prevPos.y) && cellPos != new Vector3Int(prevPos.x -1 , prevPos.y)
                    && cellPos != new Vector3Int(prevPos.x, prevPos.y +1) && cellPos != new Vector3Int(prevPos.x, prevPos.y -1))
                {
                    ClearArea(prevPos); //Clear TEMP

                    //Event for disabling UI
                    OnTurretMenuDeactivated?.Invoke();

                    _canSelect = true; //Allow Another Selection
                }
            }
        }
    }
    //Change The ID of the spawning Turret
    public void ChangeTurretID(int id)
    {
        CurrentID = id;
    }
    #endregion

    #region Tilemap Management
    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin){
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    private static void FillTiles(TileBase[] arr, TileType type)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBases[type][0];
        }
    }

    private static void SetTilesBlock(BoundsInt area,TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[(int)size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
        
    }

    #endregion

    #region Building Placement

    public void InitializeWithBuilding(GameObject building)
    {
        temp = Instantiate(building, prevPos, Quaternion.identity).GetComponent<Building>();
        temp.Data = _turretsData[CurrentID];
        temp.transform.position = GridLayout.CellToLocalInterpolated(prevPos + new Vector3(.5f, .5f, 0f));
        temp.GetComponent<Turret>().InitializeTurret(temp.Data);
    }
    
    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.Empty);
        tempTilemap.SetTilesBlock(prevArea, toClear);
    }

    private void ClearArea(Vector3Int pos)
    {
        tempTilemap.SetTile(pos, tileBases[TileType.Empty][0]);
    }

    private void FollowBuilding()
    {
        ClearArea();
        temp.area.position = GridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, mainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for(int i = 0; i < baseArray.Length; i++)
        {
            if (baseArray[i] == tileBases[TileType.White][0])
            {
                tileArray[i] = tileBases[TileType.Green][0];
            }
            else
            {
                //FillTiles(tileArray, TileType.Red);
                break;
            }
        }

        tempTilemap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, mainTilemap);
        foreach(var b in baseArray)
        {
            if(b != tileBases[TileType.White][0])
            {
                Debug.Log("You can't place here");
                return false;
            }
        }
        return true;
    }
    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.Empty, tempTilemap);
        SetTilesBlock(area, TileType.Green, mainTilemap);
    }

    #endregion

    #region Brain For Points

    private void DetectTileNear(Vector3Int cellpos)
    {
        TileBase tile = mainTilemap.GetTile(cellpos);
        if(tile == mainTilemap.GetTile(new Vector3Int(cellpos.x + 1, cellpos.y, cellpos.z)))
        {

        }
    }

    private void CompareTileType(TileBase tile, TileBase tile2, TileType type)
    {

    }
    #endregion
}
public enum TileType
{
    Empty,
    White,
    Green,
    Floor,
    Road,
    Water
}
