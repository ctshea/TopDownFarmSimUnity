using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemData
{
    public int tier;
    public int damage;
    public int energyCost;

    private void Awake()
    {    
        type = ItemType.Equipment;
    }
}
