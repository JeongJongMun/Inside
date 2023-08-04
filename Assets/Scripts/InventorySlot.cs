using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject itemObject; // ������ ���� ������Ʈ ����
    public Item item = null; // ȹ���� ������

    public void AddItem(Item _item)
    {
        // ���� �ε�
        GameObject addedItem = Resources.Load<GameObject>("Prefabs/Items/" +  _item.itemName);
        // �κ��丮�� ������ ����
        itemObject = Instantiate(addedItem, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        // �κ��丮���� Ŭ�� X
        itemObject.GetComponent<Image>().raycastTarget = false;

        item = itemObject.GetComponent<Item>();

        // (Clone) �����
        int index = item.name.IndexOf("(Clone)");
        if (index > 0) item.name = item.name.Substring(0, index);

        Debug.LogFormat("{0} added in slot", item.itemName);
    }
    public void RemoveItem()
    {
        Debug.LogFormat("{0} ������ ����", item.itemName);
        Destroy(itemObject);
        item = null;
    }
    public string GetItemName()
    {
        return item.itemName;
    }
}
