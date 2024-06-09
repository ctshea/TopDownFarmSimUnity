using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CraftableTileData : TileData
{
    public int spotsAbove;  //for the amount of space they take up
    public int spotsBelow;
    public int spotsLeft;
    public int spotsRight;
    private void Awake()
    {
        type = TileType.Craftable;
    }
}
