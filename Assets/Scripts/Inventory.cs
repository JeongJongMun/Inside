using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
static class ExtensionMethods
{
    public static bool IsContainsItem(this List<InventorySlot> list, string itemName)
    {
        foreach(InventorySlot slot in list)
        {
            if (slot.item == null) continue;
            if (slot.GetItemName() ==  itemName) return true;
        }
        return false;
    }
}

public class Inventory : MonoBehaviour
{
    // ���� ���� Inventory �ν��Ͻ��� �� instance�� ��� �༮�� ����
    // ������ ���� private
    private static Inventory instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ� �ı� X
        }
        else Destroy(gameObject);
    }

    // Inventory �ν��Ͻ��� �����ϴ� ������Ƽ
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

    public List<InventorySlot> inventory = new List<InventorySlot>();

    public bool HasItem(string itemName)
    {
        if (inventory.IsContainsItem(itemName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveItem(Item _item)
    {

    }

    public void AcquireItem(Item _item)
    {
        //Debug.LogFormat("Acquired Item Name: {0}", _item.itemName);
        //foreach (InventorySlot slot in inventory)
        //{
        //    Debug.Log("1");
        //}

        if (!inventory.IsContainsItem(_item.itemName))
        {
            foreach(InventorySlot slot in inventory)
            {
                if (slot.item == null)
                {
                    slot.AddItem(_item);
                    break;
                }
            }
        }
        else
        {
            Debug.LogFormat("�̹� {0} �������� �κ��丮�� �����Ͽ� �߰����� ����", _item.itemName);
        }
    }

    public void IsClicked(Item _item)
    {

    }


}
