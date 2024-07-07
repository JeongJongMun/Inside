using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
    
static class ExtensionMethods
{
    public static bool IsContainsItem(this List<InventorySlot> list, ItemName itemName)
    {
        foreach(InventorySlot slot in list)
        {
            if (slot == null) continue;
            if (slot.item == null) continue;
            if (slot.GetItemName() ==  itemName) return true;
        }
        return false;
    }
}

public class Inventory : MonoBehaviour
{
#region Private Variables
    private List<Toggle> toggles;
    public List<InventorySlot> inventory;
#endregion

#region Public Variables
    public static Inventory instance = null;
#endregion

#region Private Methods
    private void Awake()
    {
        instance = this;
        toggles = new List<Toggle>(GetComponentsInChildren<Toggle>());
        inventory = new List<InventorySlot>(GetComponentsInChildren<InventorySlot>());
    }
#endregion

#region Public Methods
    public bool IsClicked(ItemName _item)
    {
        if (inventory.IsContainsItem(_item))
        {
            foreach (Toggle toggle in toggles) 
            { 
                if (toggle.transform.childCount == 3 && toggle.transform.GetChild(2).GetComponent<Item>().itemName == _item && toggle.isOn)
                {
                    toggle.isOn = false;
                    return true;
                }
            }
        }
        return false;
    }

    public ItemName GetClickedItemName()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            if (toggles[i].isOn)
            {
                toggles[i].isOn = false;
                return inventory[i].GetItemName();
            }
        }
        return ItemName.None;
    }

    public void AcquireItem<T>(T item)
    {
        ItemName name = ItemName.None;
        Item _item = null;

        if (typeof(T) == typeof(Item))
        {
            _item = item as Item;
            name = _item.itemName;
        }
        else if (typeof(T) == typeof(ItemName))
        {
            name = (ItemName)(object)item;
        }

        if (!inventory.IsContainsItem(name))
        {
            foreach (InventorySlot slot in inventory)
            {
                if (slot.item == null)
                {
                    SoundManager.instance.SFXPlay("bedFabric");
                    slot.AddItem(name);
                    if (_item != null)
                        DatabaseManager.Instance.SetData(name);
                    break;
                }
            }
        }
        else
        {
            Debug.LogFormat("이미 {0}를 가지고 있습니다.", name);
        }
    }

    public void RemoveItem(ItemName itemName)
    {
        if (inventory.IsContainsItem(itemName))
        {
            foreach (InventorySlot slot in inventory)
            {
                if (slot.item.itemName == itemName)
                {
                    slot.RemoveItem();
                    SortItem();
                    break;
                }
            }
        }
        else
        {
            Debug.LogFormat("{0}가 없어 삭제할 수 없습니다. ", itemName);
        }
    }

    public void SortItem()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item == null) 
            { 
                for (int j = i; j < inventory.Count - 1; j++)
                {
                    if (inventory[j + 1].item == null) break;
                    inventory[j].AddItem(inventory[j + 1].item);
                    inventory[j + 1].RemoveItem();
                }
            }
        }
    }

    public void ClearInventory()
    {
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            if (inventory[i].item != null)
            {
                inventory[i].RemoveItem();
            }
        }
    }
#endregion
}