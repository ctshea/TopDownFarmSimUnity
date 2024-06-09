using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Paths Tile Data", menuName = "TileDatas/Paths")]
public class PathsTileData : TileData
{
    public int moveSpeed;
    private void Awake()
    {
        type = TileType.Paths;
    }
}
