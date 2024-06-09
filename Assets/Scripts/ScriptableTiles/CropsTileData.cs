using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Crops Tile Data", menuName = "TileDatas/Crops")]
public class CropsTileData : TileData
{
    public int[] daysUntilNextPhase;        //all this goes to crops
    public Tile[] tileIconPhase;
    public int[] seasons = new int[4];  //0=Spring, 1=Summer, 2= Fall, 4=Winter

    public ItemData itemData;   //this is to add the harvested crop to inventory    
    public int amountHarvested;
    private void Awake()
    {
        type = TileType.Crops;
    }
}
