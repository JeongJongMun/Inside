using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class InventorySlot : MonoBehaviour
{
    public GameObject itemObject; // ������ ���� ������Ʈ ����
    public Item item = null; // ȹ���� ������

    public void AddItem<T>(T _item)
    {
        // ���� �ε�
        GameObject addedItem = null;
        if (_item is Item)
        {
            Item item = _item as Item;
            addedItem = Resources.Load<GameObject>("Prefabs/Items/" + item.itemName);
            DatabaseManager.Instance.SetInventoryData(item.itemName, true);
        }
        else if (_item is ItemName)
        {
            addedItem = Resources.Load<GameObject>("Prefabs/Items/" + _item);
            DatabaseManager.Instance.SetInventoryData(_item, true);
        }
        // �κ��丮�� ������ ����
        itemObject = Instantiate(addedItem, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        // �κ��丮���� ���� �� ���� X
        itemObject.GetComponent<Item>().isInInventory = true;
        // �κ��丮���� Ŭ�� X
        itemObject.GetComponent<Image>().raycastTarget = false;

        item = itemObject.GetComponent<Item>();

        // (Clone) �����
        int index = item.name.IndexOf("(Clone)");
        if (index > 0) item.name = item.name.Substring(0, index);

    }

    public void RemoveItem()
    {
        Debug.LogFormat("{0} ������ ����", item.itemName);
        DatabaseManager.Instance.SetInventoryData(item.itemName, false);
        Destroy(itemObject);
        item = null;
    }
    public ItemName GetItemName()
    {
        return item.itemName;
    }
}
