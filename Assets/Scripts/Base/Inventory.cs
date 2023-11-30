using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

static class ExtensionMethods
{
    // �������� ������ �ֳ� Ȯ��
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
    public bool IsClicked(ItemName _item)
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

    // ���� Ŭ���� ������ ������ �̸� ��ȯ
    public ItemName GetClickedItemName()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (toggles[i].isOn)
            {
                toggles[i].isOn = false;
                return inventory[i].GetItemName();
            }
        }
        return ItemName.None;
    }


    // ������ ȹ��
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

        // �������� �κ��丮�� ���ٸ�
        if (!inventory.IsContainsItem(name))
        {
            foreach (InventorySlot slot in inventory)
            {
                if (slot.item == null)
                {
                    // ����ִ� �κ��丮 ���Կ� ������ ��ü �߰�
                    SoundManager.instance.SFXPlay("bedFabric");
                    slot.AddItem(name);
                    // ������ ȹ�� ���� ����
                    if (_item != null)
                        DatabaseManager.Instance.SetData(name);
                    break;
                }
            }
        }
        else
        {
            Debug.LogFormat("�̹� {0} �������� �κ��丮�� �����Ͽ� �߰����� ����", name);
        }
    }

    // ������ ����
    public void RemoveItem(ItemName itemName)
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

    // �κ��丮 ����
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
}