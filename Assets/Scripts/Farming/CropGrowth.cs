using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;     

public class CropGrowth     //data structure to hold information for each tile
{
    public Vector3Int gridPosition;
    public Tile originalTile;
    public int dayPlanted;
    public int phase = 0;
    public int daysWatered = 0;

    public CropGrowth(Vector3Int gPos, int dayPlanted, Tile originalTile)
    {
        this.gridPosition = gPos;
        this.dayPlanted = dayPlanted;
        this.phase = 0;
        this.daysWatered = 0;
        this.originalTile = originalTile;
    }

    public void phaseInc()
    {
        phase++;
    }

    public void daysWateredInc()
    {
        daysWatered++;
    }
}
