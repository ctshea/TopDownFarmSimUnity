using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmingSoil : MonoBehaviour
{

    public InventoryManager inventoryManager;
    public EnergyBarManager energyBar;
    //public GameObject player;
    public ClockControl clock;
    public TileHighlighter th;
    public getTileInfo getTile;

    public List<CropsTileData> tileDatas;
    private Dictionary<TileBase, CropsTileData> dataFromTiles;

    public List<CropGrowth> growthList;

    private TileBase[] allSoilTiles;    //for resetting soil conditions
    private TileBase[] allCropsTiles;   //for tile growth


    //TODO  get array of tiles for organization easy
    public Tile tilledTile;
    public Tile wateredTile;
    public Tile normalTile;
    public Tile planted1;

    public Tilemap cropsTileMap;
    public Tilemap soilTileMap;

    public Tilemap choppableMainTM;     //this is added to check to see if there is choppables above soil

    public Tilemap grassTM;             //this is added to check if flowers/grass must be eliminated when tilling soil


    private void Awake()
    {
        growthList = new List<CropGrowth>();
        dataFromTiles = new Dictionary<TileBase, CropsTileData>();

        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }


    void Update()                   
    {
        if (getTile.GetInRange(soilTileMap, false) || getTile.GetInRange(soilTileMap, true))
        {
            Vector3Int selectedSoilTile = soilTileMap.WorldToCell(getTile.GetTile());
            Vector3Int selectedCropsTile = cropsTileMap.WorldToCell(getTile.GetTile());
            TileBase clickedSoilTile = soilTileMap.GetTile(selectedSoilTile);
            string soilTileName = ""; 
            if (clickedSoilTile)
            {
                soilTileName = clickedSoilTile.ToString().Substring(0, clickedSoilTile.ToString().Length - 28);
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (getTile.GetInRange(soilTileMap, false))
                {
                    if (inventoryManager.getItemTypeEquipped() == "Equipment" && !choppableMainTM.HasTile(selectedSoilTile))
                    {
                        energyBar.subtractEnergy(inventoryManager.getEnergyCost());
                        if (inventoryManager.getToolEquipped() == "Hoe1")
                        {
                            if (grassTM.HasTile(selectedSoilTile))
                            {
                                grassTM.SetTile(selectedSoilTile, null);

                            }
                            if (soilTileName == "Soil 4")
                            {
                                soilTileMap.SetTile(selectedSoilTile, tilledTile);
                                //TODO need if statements to see if blocks are soil and tillable -> arraysize by tier!
                                //soilTileMap.SetTile(selectedTileAbove, tilledTile);
                                //soilTileMap.SetTile(selectedTileBelow, tilledTile);
                            }
                        }
                        else if (inventoryManager.getToolEquipped() == "WateringCan1")
                        {
                            if (soilTileName == "TilledSoil")
                            {
                                soilTileMap.SetTile(selectedSoilTile, wateredTile);
                                //TODO need if statements to see if blocks are tilled and waterable -> arraysize by tier!
                                //soilTileMap.SetTile(selectedTileAbove, wateredTile);
                                //soilTileMap.SetTile(selectedTileBelow, wateredTile);
                            }
                        }
                    }
                }
                if (getTile.GetInRange(soilTileMap, true))
                {
                    if (inventoryManager.getItemTypeEquipped() == "Seeds")
                    {
                        if (!cropsTileMap.HasTile(selectedCropsTile) && (soilTileName == "WateredSoil" || soilTileName == "TilledSoil"))
                        {
                            Debug.Log(inventoryManager.getTileData().tiles[0].ToString());
                            cropsTileMap.SetTile(selectedCropsTile, inventoryManager.getTileData().tiles[0]);
                            Tile tile = cropsTileMap.GetTile<Tile>(selectedCropsTile);
                            growthList.Add(new CropGrowth(selectedCropsTile, clock.getDay(), tile));
                        }
                    } else
                    {
                    }
                }
               
            }

            if (!choppableMainTM.HasTile(selectedSoilTile))
            {
                if ((inventoryManager.getToolEquipped() == "Hoe1" && soilTileName == "Soil 4") || (inventoryManager.getToolEquipped() == "WateringCan1" && soilTileName == "TilledSoil"))
                {
                    th.Highlight(false);
                } else if (inventoryManager.getItemTypeEquipped() == "Seeds" && !cropsTileMap.HasTile(selectedCropsTile) && (soilTileName == "WateredSoil" || soilTileName == "TilledSoil")) 
                {
                    th.Highlight(true);
                }
                else
                {
                    th.clear();
                }
            }
            else
            {
                th.clear();
            }
        } 
        else
        {
            th.clear();
        }
        
    }
    public void dailySoilCrops()                
    {
        BoundsInt bounds = soilTileMap.cellBounds;  //soil tile map as we need to work with boil soil and crops tilemaps and it encapsulates crops
        allSoilTiles = soilTileMap.GetTilesBlock(bounds);
        allCropsTiles = cropsTileMap.GetTilesBlock(bounds);
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                bool isWatered = false;
                TileBase soilTile = allSoilTiles[x + y * bounds.size.x];
                TileBase cropsTile = allCropsTiles[x + y * bounds.size.x];
                Vector3Int gridPos = new Vector3Int(x + bounds.xMin, y + bounds.yMin, bounds.z);
                if (soilTile != null)
                {
                    if (soilTile.name == "WateredSoil")
                    {
                        isWatered = true;
                        soilTileMap.SetTile(gridPos, tilledTile);
                    }
                }
                if (cropsTile != null)
                {
                    if (isWatered) growthList.Find(item => item.gridPosition == gridPos).daysWateredInc();
                    if (growthList.Find(item => item.gridPosition == gridPos).phase < dataFromTiles[growthList.Find(item => item.gridPosition == gridPos).originalTile].daysUntilNextPhase.Length)
                    {
                        if ((growthList.Find(item => item.gridPosition == gridPos).daysWatered) >= dataFromTiles[growthList.Find(item => item.gridPosition == gridPos).originalTile].daysUntilNextPhase[growthList.Find(item => item.gridPosition == gridPos).phase])
                        {
                            cropsTileMap.SetTile(gridPos, dataFromTiles[growthList.Find(item => item.gridPosition == gridPos).originalTile].tileIconPhase[growthList.Find(item => item.gridPosition == gridPos).phase]);
                            growthList.Find(item => item.gridPosition == gridPos).phaseInc();
                        }
                    }
                }
            }
        }
    }

    public bool readyToHarvest(Vector3Int pos)
    {
        TileBase cropsTile = cropsTileMap.GetTile(pos);
        if (cropsTile)
        {
            if (growthList.Find(item => item.gridPosition == pos).phase == dataFromTiles[growthList.Find(item => item.gridPosition == pos).originalTile].daysUntilNextPhase.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void removeFromGrowthList(Vector3Int pos)
    {
        growthList.Remove(growthList.Find(item => item.gridPosition == pos));
    }

    public ItemData returnItemDataToHarvest(Vector3Int pos)
    {
        return dataFromTiles[growthList.Find(item => item.gridPosition == pos).originalTile].itemData;
    }

    public int returnHarvestedAmount(Vector3Int pos)
    {
        return dataFromTiles[growthList.Find(item => item.gridPosition == pos).originalTile].amountHarvested;
    }

    public void setSoil(Vector3Int pos)
    {
        soilTileMap.SetTile(pos, normalTile);
    }
}
