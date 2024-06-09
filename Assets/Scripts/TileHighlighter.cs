using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlighter : MonoBehaviour
{
    public Tilemap highlightMap;
    public Tile highlightTile;
    public GameObject player;
    public getTileInfo getTile;

    private Vector3Int previousTile = new Vector3Int(0,0,20);
    private Vector3Int selectedHighlightTile = new Vector3Int(0,0,30); 

    public void Highlight(Vector3Int[] highlightedPos)
    {     
        for (int i = 0; i < highlightedPos.Length; i++)
        {  
            highlightMap.SetTile(highlightedPos[i], highlightTile);
        }     
    }

    public void Highlight(bool standingOn)
    {
        if (getTile.GetInRange(highlightMap, standingOn))
        {
            selectedHighlightTile = highlightMap.WorldToCell(getTile.GetTile());
            highlightMap.SetTile(selectedHighlightTile, highlightTile);

            if (selectedHighlightTile != previousTile)
            {

                highlightMap.SetTile(previousTile, null);
                highlightMap.SetTile(selectedHighlightTile, highlightTile);
                previousTile = selectedHighlightTile;

            }
        }
        else
        {
            clear();
        }
    }

    public void clear()
    {
        highlightMap.SetTile(previousTile, null);
        highlightMap.SetTile(selectedHighlightTile, null);
    }

    public void clear(Vector3Int[] highlightedPos)
    {
        for (int i = 0; i < highlightedPos.Length; i++)
        {
            highlightMap.SetTile(highlightedPos[i], null);
        }
    }


}
