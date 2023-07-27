using System.Collections.Generic;
using UnityEngine;
static class ExtensionMethods
{
    public static bool IsContainsItem(this List<InventorySlot> list, string itemName)
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
    // 게임 내에 Inventory 인스턴스는 이 instance에 담긴 녀석만 존재
    // 보안을 위해 private
    private static Inventory instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에 파괴 X
        }
        else Destroy(gameObject);
    }

    // Inventory 인스턴스에 접근하는 프로퍼티
    public static Inventory Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    // 인벤토리 슬롯 객체 배열
    public List<InventorySlot> inventory = new List<InventorySlot>();

    public bool HasItem(string _item)
    {
        if (inventory.IsContainsItem(_item))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AcquireItem(Item _item)
    {
        // 아이템이 인벤토리에 없다면
        if (!inventory.IsContainsItem(_item.itemName))
        {
            foreach (InventorySlot slot in inventory)
            {
                if (slot.item == null)
                {
                    // 비어있는 인벤토리 슬롯에 아이템 객체 추가
                    slot.AddItem(_item);
                    break;
                }
            }
        }
        else
        {
            Debug.LogFormat("이미 {0} 아이템이 인벤토리에 존재하여 추가하지 못함", _item.itemName);
        }
    }


    public void RemoveItem(Item _item)
    {

    }



    public void IsClicked(Item _item)
    {

    }


}
