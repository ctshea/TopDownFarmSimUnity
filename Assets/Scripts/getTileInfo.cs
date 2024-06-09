using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class getTileInfo : MonoBehaviour
{
    Rect screenRect = new(0, 0, Screen.width, Screen.height);
    private SpriteRenderer sr;
    private float halfHeight;
    private Vector3 posAtFeet;
    private Vector3Int[] neighbors;
    private bool inRange;
    private Vector3Int toReturn;
    int neighborsPos;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        halfHeight = sr.bounds.size.y / 2;
        posAtFeet = new Vector3(0, halfHeight, 0);
        neighbors = new Vector3Int[8]{ new Vector3Int(-1,1,0), new Vector3Int(0,1,0), new Vector3Int(1,1,0),
            new Vector3Int(-1,0,0),new Vector3Int(1,0,0),new Vector3Int(-1,-1,0),new Vector3Int(0,-1,0),new Vector3Int(1,-1,0) };
    }

    public Vector3Int GetTile()
    {       
        return toReturn;
    }

    public int getNeighborPosInt()
    {
        return neighborsPos;
    }

    public bool GetInRange(Tilemap tm, bool standingOn)
    {
        if (screenRect.Contains(Input.mousePosition))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            Vector3Int selectedMouseTile = tm.WorldToCell(pos);
            Vector3Int selectedPlayerTile = tm.WorldToCell(this.transform.position - posAtFeet);

            if (standingOn)
            {
                if (selectedMouseTile == selectedPlayerTile)
                {
                    toReturn = selectedPlayerTile;
                    inRange = true;
                    return inRange;
                }
            }
            for (int i = 0; i < neighbors.Length; i++)
            {
                if (selectedMouseTile == selectedPlayerTile + neighbors[i])
                {
                    toReturn = selectedPlayerTile + neighbors[i];
                    inRange = true;
                    neighborsPos = i;
                    break;
                } else
                {
                    inRange = false;
                }
            }
        }
        return inRange;
    }
}
