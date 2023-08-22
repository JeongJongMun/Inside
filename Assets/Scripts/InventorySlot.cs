using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject itemObject; // ������ ���� ������Ʈ ����
    public Item item = null; // ȹ���� ������

    public void AddItem<T>(T _item)
    {
        // ���� �ε�
        GameObject addedItem = null;
        if (typeof(T) == typeof(Item))
        {
            Item item = _item as Item;
            addedItem = Resources.Load<GameObject>("Prefabs/Items/" + item.itemName);
        }
        else if (typeof(T) == typeof(Define.ItemName))
        {
            addedItem = Resources.Load<GameObject>("Prefabs/Items/" + _item);
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
        Destroy(itemObject);
        item = null;
    }
    public Define.ItemName GetItemName()
    {
        return item.itemName;
    }
}
