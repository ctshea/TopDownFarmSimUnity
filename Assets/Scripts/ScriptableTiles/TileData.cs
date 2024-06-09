using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType          
{
    Crops,
    Paths,
    Choppable,
    Craftable
    //Default EASY TO ADD IN FUTURE IF NEEDED
}

public abstract class TileData : ScriptableObject
{
    public TileBase[] tiles;
    public TileType type;
}
