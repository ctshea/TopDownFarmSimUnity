using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChestTileMap : MonoBehaviour
{
    public Tile ChestMain;
    public Tile ChestBg;
    public Tilemap ChestMainTM;
    public Tilemap ChestBGTM;

    public Tilemap[] craftableCheckTM;  //tilemap[0] will always be soil

    public getTileInfo getTile;
    public InventoryManager inventoryManager;

    public Dictionary<Vector3Int, Chest> chests;
    CraftableCheck canDropCraftable;

    public ItemData rocks;  //to test

    public GameObject chestPrefab;  //this is all sprites/animation to due with chest

    private void Awake()        //declare dictionary for chests at each vector3int as key?
    {
        chests = new Dictionary<Vector3Int, Chest>();
    }

    private void Update()
    {
        if (getTile.GetInRange(ChestMainTM, false))
        {
            Vector3Int selectedChestTile = ChestMainTM.WorldToCell(getTile.GetTile());

            canDropCraftable = new CraftableCheck(craftableCheckTM, selectedChestTile);
            if (Input.GetMouseButtonDown(0))
            {
                if (inventoryManager.getToolEquipped() == "Chest")
                {
                    if (canDropCraftable.isFree())
                    {
                        ChestMainTM.SetTile(selectedChestTile, ChestMain);
                        ChestBGTM.SetTile(selectedChestTile + Vector3Int.up, ChestBg);
                        chests.Add(selectedChestTile, new Chest());
                        GameObject chestTest = Instantiate(chestPrefab, selectedChestTile, Quaternion.identity);
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))     //remove chest now
            {
                //check if chest is empty
                if (ChestMainTM.HasTile(selectedChestTile))
                {
                    //ChestMainTM.SetTile(selectedChestTile, null);
                    //ChestBGTM.SetTile(selectedChestTile + Vector3Int.up, null);
                    //chests.Remove(selectedChestTile);
                    
                    Debug.Log(chests[selectedChestTile].chestInventory.Container[0].item.name + "   " + chests[selectedChestTile].chestInventory.Container[0].amount);
                }
            }
            if (Input.GetMouseButtonDown(2))
            {
                chests[selectedChestTile].addItem(rocks, 10);
            }
        }
    }
}
