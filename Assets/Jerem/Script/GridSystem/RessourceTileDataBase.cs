using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RessourceTileDataBase: MonoBehaviour
{
    //Dico Test
    private static Dictionary<TileType, List<TileBase>> tileBases = new Dictionary<TileType, List<TileBase>>();

    //BrainLogicTileBase
    private TileBase forwardTileBase;
    private TileBase blankTileBase;
    private TileBase turnTileBase;
    private TileBase interTileBase;

    //Lists
    private List<TileBase> roadTile;
    private List<TileBase> selectionTile;
    private List<TileBase> floorTile;
    private List<TileBase> emptyTile;
    private List<TileBase> waterTile;

    //Properties
    public List<TileBase> RoadTile { get => roadTile; set => roadTile = value; }
    public List<TileBase> SelectionTile { get => selectionTile; set => selectionTile = value; }
    public List<TileBase> FloorTile { get => floorTile; set => floorTile = value; }
    public List<TileBase> EmptyTile { get => emptyTile; set => emptyTile = value; }
    public TileBase ForwardTileBase { get => forwardTileBase; set => forwardTileBase = value; }
    public TileBase BlankTileBase { get => blankTileBase; set => blankTileBase = value; }
    public TileBase TurnTileBase { get => turnTileBase; set => turnTileBase = value; }
    public TileBase InterTileBase { get => interTileBase; set => interTileBase = value; }
    public List<TileBase> WaterTile { get => waterTile; set => waterTile = value; }

    void Awake()
    {
        string _roadTilePath = @"Palette\RoadPalette\";

        BlankTileBase = Resources.Load<TileBase>(_roadTilePath + "MainRoad(blank)");
        ForwardTileBase = Resources.Load<TileBase>(_roadTilePath + "MainRoadForward(blank)");
        TurnTileBase = Resources.Load<TileBase>(_roadTilePath + "MainRoadTurn(blank)");
        InterTileBase = Resources.Load<TileBase>(_roadTilePath + "MainRoadInter(blank)");
        Debug.Log(ForwardTileBase);

        RoadTile = new List<TileBase>(3)
        {
            BlankTileBase, ForwardTileBase, TurnTileBase, InterTileBase
        };

        string _selectionPath = @"PaletteTest\";
        SelectionTile = new List<TileBase>
        {
                Resources.Load<TileBase>(_selectionPath + "SquareG"),
                Resources.Load<TileBase>(_selectionPath + "SquareB")
        };

        string _floorPath = @"PaletteTest\";
        FloorTile = new List<TileBase>
        {
                Resources.Load<TileBase>(_floorPath + "Square")
        };

        string _waterPath = @"PaletteTest\";
        WaterTile = new List<TileBase>
        {
                Resources.Load<TileBase>(_floorPath + "SquareG")
        };

        //No Path
        EmptyTile = new List<TileBase>
        {
            null
        };
    }
}
