using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chest  //do i move into a craftables or what do i do with that
{
    public InventoryObject chestInventory;
    private int inventorySize;

    public Chest()
    {
        chestInventory = ScriptableObject.CreateInstance<InventoryObject>();
    }

    public void addItem(ItemData item, int amount)
    {
        chestInventory.AddItem(item, amount);
    }

    private void OnApplicationQuit()
    {
        chestInventory.Container.Clear();
    }
}
