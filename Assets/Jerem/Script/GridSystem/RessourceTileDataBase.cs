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

    //Lists
    private List<TileBase> roadTile;
    private List<TileBase> selectionTile;
    private List<TileBase> floorTile;
    private List<TileBase> emptyTile;

    //Properties
    public List<TileBase> RoadTile { get => roadTile; set => roadTile = value; }
    public List<TileBase> SelectionTile { get => selectionTile; set => selectionTile = value; }
    public List<TileBase> FloorTile { get => floorTile; set => floorTile = value; }
    public List<TileBase> EmptyTile { get => emptyTile; set => emptyTile = value; }
    public TileBase ForwardTileBase { get => forwardTileBase; set => forwardTileBase = value; }
    public TileBase BlankTileBase { get => blankTileBase; set => blankTileBase = value; }
    public TileBase TurnTileBase { get => turnTileBase; set => turnTileBase = value; }

    void Awake()
    {
        string _roadTilePath = @"Palette\RoadPalette";

        ForwardTileBase = Resources.Load<TileBase>(_roadTilePath + "MainRoad(blank)");
        BlankTileBase = Resources.Load<TileBase>(_roadTilePath + "MainRoadForward(blank)");
        TurnTileBase = Resources.Load<TileBase>(_roadTilePath + "MainRoadTurn(blank)");

        RoadTile = new List<TileBase>
        {
            ForwardTileBase, BlankTileBase, TurnTileBase
        };

        string _selectionPath = @"PaletteTest\";
        SelectionTile = new List<TileBase>
        {
                Resources.Load<TileBase>(_selectionPath + "SquareG")        
        };

        string _floorPath = @"PaletteTest\";
        FloorTile = new List<TileBase>
        {
                Resources.Load<TileBase>(_floorPath + "Square")
        };

        //No Path
        EmptyTile = new List<TileBase>
        {
            null
        };
    }
}
