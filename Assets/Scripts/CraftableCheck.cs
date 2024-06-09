using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CraftableCheck
{
    public Tilemap[] tilemaps;
    public Vector3Int gridPosition;
    private FarmingSoil fs;

    public CraftableCheck(Tilemap[] tms, Vector3Int pos)
    {       
        this.tilemaps = tms;
        this.gridPosition = pos;
        fs = tilemaps[0].GetComponent<FarmingSoil>();
    }

    

    public bool isFree()
    {
        bool toReturn = true;
        TileBase clickedSoilTile = tilemaps[0].GetTile(gridPosition);
        string soilTileName = "";
        for (int i = 1; i < tilemaps.Length; i++)   //skip i == 0 as that is soil check
        {
            if (tilemaps[i].HasTile(gridPosition))
            {
                toReturn = false;
                break;
            }
        }
        if (toReturn)
        {
            if (clickedSoilTile)
            {
                soilTileName = clickedSoilTile.ToString().Substring(0, clickedSoilTile.ToString().Length - 28);
                if (soilTileName == "TilledSoil" || soilTileName == "WateredSoil")
                {
                    fs.setSoil(gridPosition);
                }
            }
        }
        return toReturn;
    }
}
