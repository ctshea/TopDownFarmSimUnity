using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Choppable : MonoBehaviour
{

    Rect screenRect = new(0, 0, Screen.width, Screen.height);

    public Tilemap chopTileMap;     //likely clean this up with an array of tilemaps
    public Tilemap chopBGTileMap;
    public Tilemap chopBGBottomTileMap;
    public InventoryObject inventory;

    public InventoryManager inventoryManager;
    public EnergyBarManager energyBar;
    public GameObject player;
    public PlayerMovement playerMovement;
    public TileHighlighter th;


    public ItemData rocks;      //likely clean this up with an array of itemdatas
    public ItemData wood;
    public ItemData grass;
    public getTileInfo getTile;


    public Tile rockTile;       //likely clean this up with an array of tiles
    public Tile treeTile;
    public Tile grassTile;


    private Dictionary<Vector3Int, ChoppableArrays> choppableData;

    private TileBase[] allChopTiles;

    private ScytheArea scytheSpread;

    private Vector3Int[] scytheTiles;
    private Vector3Int[] needToHighlight;   //for just highlighting hitable locations

    private Vector3Int prevPlayerPos = new Vector3Int(0, 0, 20);  //to see if player moved since last call
    private Vector3Int currentPos = new Vector3Int(0, 0, 30);
    int[] vertical = new int[] { 2, 1 };   //UPWARD vertical[1] = -2 or use -vertical[1] for downward
    int[] horizontal = new int[] { 1, 2 }; //
    int tier;
    bool playerMoved = false;
    bool gotTiles = false;
    bool playerTurned = false;

    int caseCheck = -1; //to see if cases changes on switch statement




    private void Awake()            //when more trees/rocks get added by plant or over time we can add them to dictionary above. !!!!!!!!!!!!
                                //ADD REGEN TO DICTIONARY OBJECTS
    {
        scytheSpread = new ScytheArea();
        choppableData = new Dictionary<Vector3Int, ChoppableArrays>();
        BoundsInt bounds = chopTileMap.cellBounds;   //chopTileMap contains all
        allChopTiles = chopTileMap.GetTilesBlock(bounds);
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase chopTile = allChopTiles[x + y * bounds.size.x];
                Vector3Int gridPos = new Vector3Int(x + bounds.xMin, y + bounds.yMin, bounds.z);
                if (chopTile != null)
                {
                    if (chopTile.name == "Tree 2" || chopTile.name == "Rock")   //can split into if-elses if want different starting hp
                        //LATER WHEN ADDING DIFFERENT TREES/ROCKS, CAN START WITH DIFFERENT HP HERE
                    {
                        choppableData.Add(gridPos, new ChoppableArrays(chopTileMap.GetTile<Tile>(gridPos), 25));    //25 is the base hp
                    } 
                }
            }
        }

    }


    private void Update()
    {
        playerTurned = playerMovement.getPlayerTurned();    //to see if tiles must be cleared
        currentPos = chopTileMap.WorldToCell(player.gameObject.transform.position);
        if (prevPlayerPos != currentPos)
        {
            playerMoved = true;
            prevPlayerPos = currentPos;
        }
        
        if (getTile.GetInRange(chopTileMap, false))
        {
            Vector3Int selectedChopTile = chopTileMap.WorldToCell(getTile.GetTile());
            TileBase clickedChopTile = chopTileMap.GetTile(selectedChopTile);
            string chopTileName = "";
            if (clickedChopTile)
            {
                chopTileName = clickedChopTile.ToString().Substring(0, clickedChopTile.ToString().Length - 28);
            }
            if (Input.GetMouseButtonDown(2))
            {
                
                    if (inventoryManager.getItemTypeEquipped() == "Equipment")
                    {
                        energyBar.subtractEnergy(inventoryManager.getEnergyCost());
                        if (inventoryManager.getToolEquipped() == "Axe1")
                        {
                            if (chopTileName == "Tree 2")           //need to give trees and rocks HP and have weapons do damage so it takes hits // do that thing for TileData to extend ADD TO INV
                            {
                                choppableData[selectedChopTile].subtractHp(inventoryManager.getDamage());
                                if (choppableData[selectedChopTile].getHp() <= 0)
                                {
                                    choppableData.Remove(selectedChopTile);
                                    chopTileMap.SetTile(selectedChopTile, null);
                                    chopBGBottomTileMap.SetTile(selectedChopTile + new Vector3Int(0, -1, 0), null);
                                    chopBGTileMap.SetTile(selectedChopTile + new Vector3Int(0, 1, 0), null);
                                    inventory.AddItem(wood, 3);
                                }
                            }
                        }
                        else if (inventoryManager.getToolEquipped() == "Pickaxe1")
                        {
                            if (chopTileName == "Rock")  
                            {
                                Debug.Log(choppableData[selectedChopTile].getHp());
                                choppableData[selectedChopTile].subtractHp(inventoryManager.getDamage());
                                if (choppableData[selectedChopTile].getHp() <= 0)
                                {
                                    choppableData.Remove(selectedChopTile);
                                    chopTileMap.SetTile(selectedChopTile, null);
                                    inventory.AddItem(rocks, 2);
                                }
                            }
                        }
                        else if (inventoryManager.getToolEquipped() == "Sword1")        //this may be scythe aim that way in this block, in playerdirection out of main if statement
                        {                          
                            tier = 1;
                            switch (getTile.getNeighborPosInt())
                            {
                                case 1: //top middle
                                    scytheSwing(tier, "up");
                                    break;
                                case 0:
                                case 3:
                                case 5:
                                    scytheSwing(tier, "left");
                                    break;
                                case 6:
                                    scytheSwing(tier, "down");
                                break;
                                default:
                                    scytheSwing(tier, "right"); ;
                                    break;
                            }
                        scytheTileRemoval();
                        }
                    }
                
                               
            }
            if (inventoryManager.getToolEquipped() == "Sword1") //this is for AIMING WHEN ITS CLOSE ENOUGH TO PLAYER
            {
                if (gotTiles)
                {
                    th.clear(scytheTiles);
                }
                tier = 1;
                switch (getTile.getNeighborPosInt())
                {
                    case 1: //top middle
                        if (caseCheck != 1)
                        {
                            caseCheck = 1;
                            if (gotTiles)
                            {
                                th.clear(scytheTiles);
                            }
                        }
                        scytheSwing(tier, "up");
                        break;
                    case 0:
                    case 3:
                    case 5:
                        if (caseCheck != 0 && caseCheck != 3 && caseCheck != 5)
                        {
                            caseCheck = 3;
                            if (gotTiles)
                            {
                                th.clear(scytheTiles);
                            }
                        }
                        scytheSwing(tier, "left");
                        break;
                    case 6:
                        if (caseCheck != 6)
                        {
                            caseCheck = 6;
                            if (gotTiles)
                            {
                                th.clear(scytheTiles);
                            }
                        }
                        scytheSwing(tier, "down");
                        break;
                    default:
                        if (caseCheck != 2 && caseCheck != 4 && caseCheck != 7)
                        {
                            caseCheck = 4;
                            if (gotTiles)
                            {
                                th.clear(scytheTiles);
                            }
                        }
                        scytheSwing(tier, "right"); ;
                        break;
                }
                if (gotTiles)
                {
                    if ((playerMoved || playerTurned) || (playerMoved && playerTurned))
                    {
                        if (playerMoved) playerMoved = false;
                        th.clear(scytheTiles);
                    }
                }
                th.Highlight(scytheTiles);
            } else
            {
                if (gotTiles)
                {
                    th.clear(scytheTiles);
                }
            }

        }
        else if ((screenRect.Contains(Input.mousePosition)))
        {
            if (gotTiles)
            {
                th.clear(scytheTiles);
            }
            if (inventoryManager.getToolEquipped() == "Sword1")     //also needs to constantly highlight for sword use
            {
                tier = 1;
                if (Input.GetMouseButtonDown(2))
                {
                    scytheSwing(tier, playerMovement.getPlayerDirection());
                    scytheTileRemoval();
                }
                if (gotTiles)
                    if ((playerMoved || playerTurned) || (playerMoved && playerTurned))
                    {
                        if (playerMoved) playerMoved = false;
                        th.clear(scytheTiles);
                    }
                scytheSwing(tier, playerMovement.getPlayerDirection());
                th.Highlight(scytheTiles);
            }
            else
            {
                if (gotTiles)
                {
                    th.clear(scytheTiles);
                }
            }
        }
        else
        {
            if (gotTiles)
            {
                th.clear(scytheTiles);
            }
        }
    }

    public void scytheSwing(int tier, string direction)
    {
        
        if (direction =="up")
        {           
            scytheTiles = scytheSpread.getTiles(tier, tier * vertical[0], tier * vertical[1], player, chopTileMap);
        }
        else if (direction == "down")
        {
            scytheTiles = scytheSpread.getTiles(tier, tier * vertical[0], tier * -vertical[1], player, chopTileMap);
        } else if (direction == "right")
        {
            scytheTiles = scytheSpread.getTiles(tier, tier * horizontal[0], tier * horizontal[1], player, chopTileMap);
        } else          //facing left
        {
            scytheTiles = scytheSpread.getTiles(tier, tier * -horizontal[0], tier * horizontal[1], player, chopTileMap);
        }
        gotTiles = true;
    }

    public void scytheTileRemoval()
    {
        for (int i = 0; i < scytheTiles.Length; i++)
        {
            string scytheTileName = "";
            TileBase scytheTile = chopTileMap.GetTile(scytheTiles[i]);
            if (scytheTile)
            {
                scytheTileName = scytheTile.ToString().Substring(0, scytheTile.ToString().Length - 28);
            }
            if (scytheTileName == "grass")
            {
                inventory.AddItem(grass, 1);
                chopTileMap.SetTile(scytheTiles[i], null);
            }
        }
    } 
}

