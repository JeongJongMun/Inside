using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject itmeObject; // ������ ���� ������Ʈ ����
    public Item item = null; // ȹ���� ������

    public void AddItem(Item _item)
    {
        Debug.Log(_item.itemName);
        GameObject addedItem = Resources.Load<GameObject>("Prefabs/Items/" +  _item.itemName);
        itmeObject = Instantiate(addedItem, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        item = itmeObject.GetComponent<Item>();

        // (Clone) �����
        int index = item.name.IndexOf("(Clone)");
        if (index > 0) item.name = item.name.Substring(0, index);


        Debug.LogFormat("{0} added in slot", item.itemName);
    }
    public void RemoveItem()
    {
        Debug.LogFormat("{0} ������ ����", item.itemName);
        Destroy(itmeObject);
        item = null;
    }
    public string GetItemName()
    {
        return item.itemName;
    }
}
