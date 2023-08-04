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
    // �ٸ� ��ũ��Ʈ���� ���� ��� : Inventory.Instanace.�޼ҵ�()
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

    [Header("������ ��� �迭")]
    public List<Toggle> toggles = new List<Toggle>();

    [Header("�κ��丮 ���� �迭")]
    public List<InventorySlot> inventory = new List<InventorySlot>();


    // �ش� �������� ��Ŭ�� Ŭ���Ǿ� �ֳ� Ȯ��
    public bool IsClicked(string _item)
    {
        if (inventory.IsContainsItem(_item))
        {
            foreach (Toggle toggle in toggles) 
            { 
                // childCount == 3 �϶� (�������� �Ծ��� ������) �� ����
                if (toggle.transform.childCount == 3 && toggle.transform.GetChild(2).GetComponent<Item>().itemName == _item && toggle.isOn)
                {
                    toggle.isOn = false;
                    return true;
                }
            }
        }
        return false;
    }

    // �������� ������ �ֳ� Ȯ��
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

    // ������ ȹ��
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


    // ������ ����
    public void RemoveItem(string itemName)
    {
        // �������� �κ��丮�� �ִٸ�
        if (inventory.IsContainsItem(itemName))
        {
            foreach (InventorySlot slot in inventory)
            {
                if (slot.item.itemName == itemName)
                {
                    // ���Կ��� ������ ����
                    slot.RemoveItem();
                    // ������ ����
                    SortItem();
                    break;
                }
            }
        }
        else
        {
            Debug.LogFormat("{0} �������� �κ��丮�� �������� �ʾ� �������� ����", itemName);
        }
    }

    // ������ ���/���� �� ����
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