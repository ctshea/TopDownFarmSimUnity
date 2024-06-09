using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Seeds,
    Equipment,
    Default
}
public abstract class ItemData : ScriptableObject
{
    public Sprite icon;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
    public int maxStackSize;
}
