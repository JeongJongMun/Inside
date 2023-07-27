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

    // �κ��丮 ���� ��ü �迭
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
        // �������� �κ��丮�� ���ٸ�
        if (!inventory.IsContainsItem(_item.itemName))
        {
            foreach (InventorySlot slot in inventory)
            {
                if (slot.item == null)
                {
                    // ����ִ� �κ��丮 ���Կ� ������ ��ü �߰�
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


    public void RemoveItem(Item _item)
    {

    }



    public void IsClicked(Item _item)
    {

    }


}
