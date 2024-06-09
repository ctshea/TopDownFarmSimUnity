using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]

public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public int maxInventorySize = 12;
    public void AddItem(ItemData _item, int _amount)
    {
        bool hasItem = false;
        for(int i = 0; i < Container.Count; i++)
        {
            if (Container[i].amount == Container[i].item.maxStackSize) //its full, cant add more so ignore
            {
                Container[i].isFull = true;
            }
            if (Container[i].item == _item && !Container[i].isFull)
            {
                if (Container[i].amount + _amount > Container[i].item.maxStackSize) //split into 2 stacks or just add new if full
                {
                    {
                        int amountDif = Container[i].item.maxStackSize - Container[i].amount;
                        Container[i].AddAmount(amountDif);
                       
                        Container.Add(new InventorySlot(_item, _amount - amountDif));
                    }

                } else
                {
                    Container[i].AddAmount(_amount);
                }
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }
    }

}

[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int amount;
    public bool isFull = false;
    public InventorySlot(ItemData _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}