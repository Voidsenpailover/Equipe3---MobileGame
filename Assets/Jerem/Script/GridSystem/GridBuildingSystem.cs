using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class GridBuildingSystem : MonoBehaviour
{
    //Instantiate
    [SerializeField] public static GridBuildingSystem current;

    //Events
    public static event Action<Vector3> OnSelectionMenuActive;
    public static event Action OnSelectionMenuDeactivated;
    public static event Action OnTurretMenuActivated;

    public static event Action<Vector3> OnInfoMenuActive;
    public static event Action<TurretsData> OnInfoMenuDragActive;
    public static event Action OnInfoMenuDeactivated;

    public static event Action<Vector3> OnFusionMenuActive;
    public static event Action OnFusionMenuDeactivated;

    public static event Action<Vector3Int> OnPointCreated;
    public static event Action OnRoadEnd;

    //TileMaps
    [SerializeField] private GridLayout gridLayout;
    [SerializeField] Tilemap mainTilemap;
    [SerializeField] Tilemap tempTilemap;

    //TileBase
    private static List<TileBase> _tiles;
    private static Dictionary<TileType, List<TileBase>> tileBases = new Dictionary<TileType, List<TileBase>>();
    private static Dictionary<Vector3Int, GameObject> tileDataBases = new Dictionary<Vector3Int, GameObject>();
    [SerializeField] private RessourceTileDataBase _sourceTileData;

    //TilesPos
    private Building temp;
    private Vector3Int prevPos;
    private BoundsInt prevArea;

    private Vector3Int spawningPos;
    [SerializeField] private Transform spawningPosT;

    //Turrets Data
    private int _currentID;
    [SerializeField] private List<TurretsData> _turretsData;

    //Properties
    public GridLayout GridLayout { get => gridLayout; set => gridLayout = value; }
    public int CurrentID { get => _currentID; set => _currentID = value; }
    public bool CanDrag { get => _canDrag; set => _canDrag = value; }
    public bool IsDraggingNow { get => _isDraggingNow; set => _isDraggingNow = value; }
    public bool CanSelect { get => _canSelect; set => _canSelect = value; }
    public List<TurretsData> TurretsData { get => _turretsData; set => _turretsData = value; }

    //Selections
    private bool _canSelect;
    private bool _canDrag = false;
    private bool _isDraggingNow = false;

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
        CanSelect = true;

        spawningPos = GridLayout.LocalToCell(spawningPosT.position);

        Vector3Int comparePos = Vector3Int.zero;
        Vector3Int cellpos = spawningPos;
        Vector3Int tempPos = Vector3Int.zero;
        OnPointCreated?.Invoke(cellpos);
        while (comparePos != cellpos)
        {
            tempPos = DetectTileNear(cellpos, comparePos, 0);
            if(tempPos == cellpos)
            {
                tempPos = DetectTileNear(cellpos, comparePos, 3);
                if (tempPos != cellpos)
                {
                    Vector3Int convertPosForInter = new Vector3Int(tempPos.x - cellpos.x, tempPos.y - cellpos.y, tempPos.z - cellpos.z);
                    comparePos = tempPos;
                    tempPos += convertPosForInter;
                    cellpos = tempPos;
                    Debug.Log("OVERLAP" + " " + cellpos);
                    continue;
                }
                tempPos = DetectTileNear(cellpos, comparePos, 2);
                if( tempPos != cellpos)
                {
                    //Instantiate POINT
                    OnPointCreated?.Invoke(tempPos);
                    Debug.Log("POINT" +" "+ tempPos);
                }
            }
            comparePos = cellpos;
            cellpos = tempPos;
            Debug.Log("Coord" + " " + cellpos);
        }
        OnPointCreated?.Invoke(cellpos);
        OnRoadEnd?.Invoke();

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

            //If it's beyond grid
            if (tileSelected == tileBases[TileType.Empty][0]) 
            {
                return;
            }
            //If it's road
            for (int i = 0; i < tileBases[TileType.Road].Count; i++)
            {
                if (tileSelected == tileBases[TileType.Road][i])
                {
                    return;
                }
            }

            //Selection
            if (CanSelect) // Check For selection
            {
                CanSelect = false; //Un-allow Spaming
                ClearArea(); //Clear Tiles for TEMP
                prevPos = cellPos; //Keep Pos For Further utility

                //Selection TILE
                /*
                TileBase tileTex = tileBases[TileType.Green][0]; 
                tempTilemap.SetTile(cellPos, tileTex);
                */

                //Event for UI
                //If it's tower
                if (tileSelected == tileBases[TileType.Green][1])
                {
                    OnInfoMenuActive?.Invoke(GridLayout.CellToLocalInterpolated(cellPos + new Vector3(.5f, .5f, 0f)));
                    CanDrag = true;
                }
                else
                {
                    OnSelectionMenuActive?.Invoke(GridLayout.CellToLocalInterpolated(cellPos + new Vector3(.5f, .5f, 0f)));
                }
            }
            else //UI already UP
            {
                //Check if its not a fusion mode
                if(mainTilemap.GetTile(prevPos) != tileBases[TileType.Green][1])
                {
                    //Check for UP/DOWN/LEFT/RIGHT Pos For Buttons
                    if (cellPos != prevPos && cellPos != new Vector3Int(prevPos.x + 1, prevPos.y) && cellPos != new Vector3Int(prevPos.x - 1, prevPos.y)
                        && cellPos != new Vector3Int(prevPos.x, prevPos.y + 1) && cellPos != new Vector3Int(prevPos.x, prevPos.y - 1))
                    {
                        ClearArea(prevPos); //Clear TEMP

                        //Event for disabling UI
                        OnSelectionMenuDeactivated?.Invoke();

                        CanSelect = true; //Allow Another Selection
                    }
                }
                else
                {
                    if (cellPos == prevPos && CanDrag)
                    {
                        OnInfoMenuDeactivated?.Invoke();
                        if (tileDataBases[cellPos] != null)
                        {
                            Debug.Log(tileDataBases[cellPos].GetComponent<Building>().Data.Level);
                            OnInfoMenuDragActive?.Invoke(tileDataBases[cellPos].GetComponent<Building>().Data);
                        }
                        IsDraggingNow = true;
                    }
                    else
                    {
                        OnInfoMenuDeactivated?.Invoke();
                        CanSelect = true;
                    }
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
        temp.Data = TurretsData[CurrentID];
        temp.transform.position = GridLayout.CellToLocalInterpolated(prevPos + new Vector3(.5f, .5f, 0f));
        temp.GetComponent<Turret>().InitializeTurret(temp.Data);
        mainTilemap.SetTile(gridLayout.WorldToCell(temp.transform.position), tileBases[TileType.Green][1]);
        tileDataBases.Add(gridLayout.WorldToCell(temp.transform.position), temp.transform.gameObject);
        OnSelectionMenuDeactivated?.Invoke();
        CanSelect = true;
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
    //DetectIfTileNearIsATurn
    private Vector3Int DetectTileNear(Vector3Int cellpos, Vector3Int prevpos, int id)
    {
        TileBase tile = mainTilemap.GetTile(cellpos);
        Vector3Int temp;
        temp = new Vector3Int(cellpos.x + 1, cellpos.y, cellpos.z);
        if (CompareTileTypeID(mainTilemap.GetTile(temp),TileType.Road, id))
        {
            if(temp != prevpos)
            {
                return temp;
            }
        }
        temp = new Vector3Int(cellpos.x - 1, cellpos.y, cellpos.z);
        if (CompareTileTypeID(mainTilemap.GetTile(temp), TileType.Road, id))
        {
            if (temp != prevpos)
            {
                return temp;
            }
        }
        temp = new Vector3Int(cellpos.x, cellpos.y + 1, cellpos.z);
        if (CompareTileTypeID(mainTilemap.GetTile(temp), TileType.Road, id))
        {
            if (temp != prevpos)
            {
                return temp;
            }
        }
        temp = new Vector3Int(cellpos.x, cellpos.y - 1, cellpos.z);
        if (CompareTileTypeID(mainTilemap.GetTile(temp), TileType.Road, id))
        {
            if (temp != prevpos)
            {
                return temp;
            }
        }
        return cellpos;
    }
    //Compare Tile with ID
    private bool CompareTileTypeID(TileBase tile, TileType type, int typeID)
    {
        if(tile == tileBases[type][typeID])
        {
            return true;
        }
        return false;
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
