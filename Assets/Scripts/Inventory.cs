using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    // 다른 스크립트에서 접근 방법 : Inventory.Instanace.메소드()
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

    [Header("아이템 토글 배열")]
    public List<Toggle> toggles = new List<Toggle>();

    [Header("인벤토리 슬롯 배열")]
    public List<InventorySlot> inventory = new List<InventorySlot>();


    // 해당 아이템의 토클이 클릭되어 있나 확인
    public bool IsClicked(string _item)
    {
        if (inventory.IsContainsItem(_item))
        {
            foreach (Toggle toggle in toggles) 
            { 
                // childCount == 3 일때 (아이템이 먹어져 있을때) 만 접근
                if (toggle.transform.childCount == 3 && toggle.transform.GetChild(2).GetComponent<Item>().itemName == _item && toggle.isOn)
                {
                    toggle.isOn = false;
                    return true;
                }
            }
        }
        return false;
    }

    // 아이템을 가지고 있나 확인
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

    // 아이템 획득
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


    // 아이템 삭제
    public void RemoveItem(string itemName)
    {
        // 아이템이 인벤토리에 있다면
        if (inventory.IsContainsItem(itemName))
        {
            foreach (InventorySlot slot in inventory)
            {
                if (slot.item.itemName == itemName)
                {
                    // 슬롯에서 아이템 삭제
                    slot.RemoveItem();
                    // 아이템 정렬
                    SortItem();
                    break;
                }
            }
        }
        else
        {
            Debug.LogFormat("{0} 아이템이 인벤토리에 존재하지 않아 삭제하지 못함", itemName);
        }
    }

    // 아이템 사용/삭제 시 정렬
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
}