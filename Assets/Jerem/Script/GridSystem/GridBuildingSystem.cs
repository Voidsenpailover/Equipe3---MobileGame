using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] public static GridBuildingSystem current;

    public static event Action<Vector3> OnTurretMenuActive;
    public static event Action OnTurretMenuDeactivated;

    [SerializeField] private GridLayout gridLayout;
    [SerializeField] Tilemap mainTilemap;
    [SerializeField] Tilemap tempTilemap;

    private int _currentID;
    [SerializeField] private List<TurretsData> _turretsData;

    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase> ();

    private Building temp;
    private Vector3Int prevPos;
    private BoundsInt prevArea;

    private bool _canSelect;

    public GridLayout GridLayout { get => gridLayout; set => gridLayout = value; }
    public int CurrentID { get => _currentID; set => _currentID = value; }

    #region Unity Methods

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        string _tilePath = @"PaletteTest\";
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(_tilePath + "SquareG"));
        tileBases.Add(TileType.White, Resources.Load<TileBase>(_tilePath + "Square"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(_tilePath + "SquareR"));
        _canSelect = true;
        
    }

    private void Update()
    {

        float x = GridLayout.cellSize.x * Camera.main.pixelWidth / 1080;
        float y = GridLayout.cellSize.y * Camera.main.pixelHeight / 1920;

        /*if (!temp)
        {
            return;
        }*/
        if(Input.GetMouseButtonDown(0))
        {
            if (_canSelect) {
                _canSelect = false;
                ClearArea();
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) * 1 / GridLayout.transform.localScale.x;
                Vector3Int cellPos = GridLayout.LocalToCell(touchPos);
                prevPos = cellPos;

                TileBase test = mainTilemap.GetTile(cellPos);
                test = tileBases[TileType.Green];
                tempTilemap.SetTile(cellPos, test);
                OnTurretMenuActive?.Invoke(GridLayout.CellToLocalInterpolated(cellPos + new Vector3(.5f, .5f, 0f)));
                
                Debug.Log(test);
                /*
                if (!temp.Placed)
                {
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) * 1/GridLayout.transform.localScale.x;
                    Vector3Int cellPos = GridLayout.LocalToCell(touchPos);
                    Debug.Log(cellPos);
                    Debug.Log(touchPos);

                    if (prevPos != cellPos)
                    {
                        temp.transform.localPosition = GridLayout.CellToLocalInterpolated(cellPos
                            + new Vector3(.5f ,.5f, 0f)) * GridLayout.transform.localScale.x;
                        prevPos = cellPos;
                        FollowBuilding();
                    }
                }
                */
            }
            else
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) * 1 / GridLayout.transform.localScale.x;
                Vector3Int cellPos = GridLayout.LocalToCell(touchPos);
                if(cellPos != prevPos && cellPos != new Vector3Int(prevPos.x +1, prevPos.y) && cellPos != new Vector3Int(prevPos.x -1 , prevPos.y)
                    && cellPos != new Vector3Int(prevPos.x, prevPos.y +1) && cellPos != new Vector3Int(prevPos.x, prevPos.y -1))
                {
                    ClearArea(prevPos);
                    OnTurretMenuDeactivated?.Invoke();
                    _canSelect = true;
                }
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            /*if (temp.CanBePlaced()){
                temp.Place();
            }*/
            ClearArea();
            _canSelect = true;
        }
        /*
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            ClearArea();
            Destroy(temp.gameObject);
        }*/
    }

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
            arr[i] = tileBases[type];
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
        temp = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>();
        temp.Data = _turretsData[CurrentID];
        temp.area.position = GridLayout.WorldToCell(temp.gameObject.transform.position);
        FollowBuilding();
    }
    
    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.Empty);
        tempTilemap.SetTilesBlock(prevArea, toClear);
    }

    private void ClearArea(Vector3Int pos)
    {
        tempTilemap.SetTile(pos, tileBases[TileType.Empty]);
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
            if (baseArray[i] == tileBases[TileType.White])
            {
                tileArray[i] = tileBases[TileType.Green];
            }
            else
            {
                FillTiles(tileArray, TileType.Red);
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
            if(b != tileBases[TileType.White])
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
}
public enum TileType
{
    Empty,
    White,
    Green,
    Red
}
