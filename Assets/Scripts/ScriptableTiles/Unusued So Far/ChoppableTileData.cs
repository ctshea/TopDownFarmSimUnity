using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Choppable Tile Data", menuName = "TileDatas/Choppable")]
public class ChoppableTileData : TileData
{
    public int healthPoints;
    private void Awake()
    {
        type = TileType.Choppable;
    }

}
