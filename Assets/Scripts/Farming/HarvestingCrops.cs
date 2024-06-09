using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HarvestingCrops : MonoBehaviour
{
    public Tilemap cropsTileMap;
    public InventoryObject inventory;
    public FarmingSoil farmingSoil;
    public getTileInfo getTile;


    private void Update()
    {
        if (getTile.GetInRange(cropsTileMap, true))
        {
            Vector3Int selectedCropsTile = cropsTileMap.WorldToCell(getTile.GetTile());
            if (Input.GetMouseButtonDown(1))
            {
                if (farmingSoil.readyToHarvest(selectedCropsTile))
                {
                    cropsTileMap.SetTile(selectedCropsTile, null);
                    inventory.AddItem(farmingSoil.returnItemDataToHarvest(selectedCropsTile), farmingSoil.returnHarvestedAmount(selectedCropsTile));  //need a way to know which item to add
                    farmingSoil.removeFromGrowthList(selectedCropsTile);
                }
            }
        }
    }
}
