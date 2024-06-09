using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScytheArea
{
    public Vector3Int[] test;
    int arrayCounter = 0;
    public Vector3Int[] getTiles(int tier, int x, int y, GameObject player, Tilemap tm)
    {
        Vector3Int selectedPlayerTile = tm.WorldToCell(player.gameObject.transform.position);
        
        int startpoint = 0;
        int endpoint = 0;
        
        int max = 0;
        int min;
        {
            if (tier == 1)  //second tier <- WORK THIS INTO TRIMMING EDGES FOR MULTIPLE TIERS. thinking 3 tiers rn before harvester, also have to figure horizontal algorithm
            {
                test = new Vector3Int[5];
            }
            else if (tier == 2)
            {
                test = new Vector3Int[12];
            } else {    //tier 3
                test = new Vector3Int[21];
            }
            if (x == Mathf.Max(x, y))    //x is the larger of the two
            {
                max = (x / 2);
                min = y;
                if (y < 0)              //y is negative
                {
                    startpoint = 0;
                    endpoint = y;
                }
                else
                {
                    startpoint = y;
                    endpoint = 0;
                }
                for (int row = -max; row <= max; row++) //X           if x is higher
                {
                    for (int col = startpoint; col >= endpoint; col--)  //Y
                    {
                        Vector3Int toAdd = new Vector3Int(selectedPlayerTile.x + row, selectedPlayerTile.y + col, 0);
                        TileBase clickedTile = tm.GetTile(toAdd);
                        if ((Mathf.Abs(row) > (int)((float)max * ((float)1 / (float)max))) &&
                                (Mathf.Abs(col) > (int)((float)min * ((float)1 / (float)min))) || (col == 0 && row == 0))// && (Mathf.Abs(col) > min * ((tier - 1) * (1 / min))))
                        {
                            if (tier > 1 && (Mathf.Abs(col) == Mathf.Abs(min) - 1 && Mathf.Abs(row) == Mathf.Abs(max) - 1))
                            {
                                //if its grass tile add
                                test[arrayCounter++] = toAdd;
                            }

                            continue;
                        }
                        else //if its grass tile add
                            test[arrayCounter++] = toAdd;
                    }
                }
            } else          //y is the larger of the two
            {
                max = (y / 2);
                min = x;
                if (x < 0)              //x is negative
                {
                    startpoint = 0;
                    endpoint = x;
                }
                else
                {
                    startpoint = x;
                    endpoint = 0;
                }
                for (int row = startpoint; row >= endpoint; row--) //X      //if y is higher
                {
                    for (int col = -max; col <= max; col++)  //Y
                    {
                        if ((Mathf.Abs(row) > (int)((float)max * ((float)1 / (float)max))) &&
                                (Mathf.Abs(col) > (int)((float)min * ((float)1 / (float)min))) || (col == 0 && row == 0))// && (Mathf.Abs(col) > min * ((tier - 1) * (1 / min))))
                        {
                            if (tier > 1 && (Mathf.Abs(col) == Mathf.Abs(min) - 1 && Mathf.Abs(row) == Mathf.Abs(max) - 1))
                            {
                                test[arrayCounter++] = new Vector3Int(selectedPlayerTile.x + row, selectedPlayerTile.y + col, 0);
                            }

                            continue;
                        }
                        else
                            test[arrayCounter++] = new Vector3Int(selectedPlayerTile.x + row, selectedPlayerTile.y + col, 0);
                    }
                }
            }                               
        }
        arrayCounter = 0;
        return test;
    }
}
