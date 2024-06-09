using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] inventory;
    public Image[] inventoryslots;
    public TextMeshProUGUI[] TMPro;
    public InventoryObject playerInventory;

    Dictionary<InventorySlot, Image> itemsDisplayed = new Dictionary<InventorySlot, Image>();


    Outline[] outlines = new Outline[12];

    int invPos = 0;
    Vector2 invVector = new Vector2(5, 5);
    
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
        for (int i = 0; i < inventory.Length; i++)
        {
            outlines[i] = inventory[i].GetComponent<Outline>();
        }
        RenderNewLine(invPos);
    }

    // Update is called once per frame
    void Update()
    {
       
        

        UpdateDisplay();                                //TODO FOR EFFICIENCY, ONLY UPDATE DISPLAY ON ITEM PICKUP OR REMOVAL
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (invPos > 0) invPos--;
            ClearOutlines(invPos+1);
            RenderNewLine(invPos);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (invPos < 11) invPos++;
            ClearOutlines(invPos-1);
            RenderNewLine(invPos);
        }

        if (Input.GetKeyDown("1"))
        {
            ClearOutlines(invPos);
            invPos = 0;
            RenderNewLine(invPos);
        }
        else if (Input.GetKeyDown("2"))
        {
            ClearOutlines(invPos);
            invPos = 1;
            RenderNewLine(invPos);
        }
        else if (Input.GetKeyDown("3"))
        {
            ClearOutlines(invPos);
            invPos = 2;
            RenderNewLine(invPos);
        }
        else if (Input.GetKeyDown("4"))
        {
            ClearOutlines(invPos);
            invPos = 3;
            RenderNewLine(invPos);
        }
        else if (Input.GetKeyDown("5"))
        {
            ClearOutlines(invPos);
            invPos = 4;
            RenderNewLine(invPos);
        }
        else if (Input.GetKeyDown("6"))
        {
            ClearOutlines(invPos);
            invPos = 5;
            RenderNewLine(invPos);
        }
        else if (Input.GetKeyDown("7"))
        {
            ClearOutlines(invPos);
            invPos = 6;
            RenderNewLine(invPos);
        }
        else if (Input.GetKeyDown("8"))
        {
            ClearOutlines(invPos);
            invPos = 7;
            RenderNewLine(invPos);
        }
        else if (Input.GetKeyDown("9"))
        {
            ClearOutlines(invPos);
            invPos = 8;
            RenderNewLine(invPos);
        }
        else if (Input.GetKeyDown("0"))
        {
            ClearOutlines(invPos);
            invPos = 9;
            RenderNewLine(invPos);
        }
        else if (Input.GetKeyDown("-"))
        {
            ClearOutlines(invPos);
            invPos = 10;
            RenderNewLine(invPos);
        }
        else if (Input.GetKeyDown("="))
        {
            ClearOutlines(invPos);
            invPos = 11;
            RenderNewLine(invPos);
        }
    }   

    public void RenderNewLine(int pos)
    {
        outlines[pos].effectDistance = invVector;
    }

    public void ClearOutlines(int pos)
    {
        outlines[pos].effectDistance = Vector2.zero;
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < playerInventory.Container.Count; i++)
        {
            inventoryslots[i].sprite = playerInventory.Container[i].item.icon;
            TMPro[i].SetText(playerInventory.Container[i].amount.ToString());
            itemsDisplayed.Add(playerInventory.Container[i], inventoryslots[i]);
        }
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < playerInventory.Container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(playerInventory.Container[i]))
            {
                TMPro[i].SetText(playerInventory.Container[i].amount.ToString());
            }
            else
            {
                inventoryslots[i].sprite = playerInventory.Container[i].item.icon;                
                itemsDisplayed.Add(playerInventory.Container[i], inventoryslots[i]);
                TMPro[i].SetText(playerInventory.Container[i].amount.ToString());
            }
        }
    }

    public int getInvPos()
    {
        return invPos;
    }

    public string getToolEquipped()
    {
        if (invPos < playerInventory.Container.Count)
        {
            return playerInventory.Container[invPos].item.name;
        }
        else return "empty";
    }

    public string getItemTypeEquipped()
    {
        if (invPos < playerInventory.Container.Count)
        {
            return playerInventory.Container[invPos].item.type.ToString();
        }
        else return "empty";
    }

    public int getEnergyCost()
    {
        EquipmentObject eObject = (EquipmentObject)playerInventory.Container[invPos].item;
        return eObject.energyCost;
    }

    public CropsTileData getTileData()
    {
        SeedsObject sObject = (SeedsObject)playerInventory.Container[invPos].item;
        return sObject.tile;
    }

    public int getDamage()
    {
        EquipmentObject eObject = (EquipmentObject)playerInventory.Container[invPos].item;
        return eObject.damage;
    }
}
