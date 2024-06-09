using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seeds Object", menuName = "Inventory System/Items/Seeds")]
public class SeedsObject : ItemData
{
    public CropsTileData tile;

    private void Awake()
    {
        type = ItemType.Seeds;
    }
}
