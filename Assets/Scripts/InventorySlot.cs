using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item = null; // ȹ���� ������
    public Image currentItemImage = null; // ���� ȹ���� �������� �̹���

    private void Start()
    {
        currentItemImage = this.transform.GetChild(2).transform.GetComponent<Image>();
    }

    public void AddItem(Item _item)
    {
        item = _item;
        currentItemImage = item.itemImage;
        Debug.LogFormat("InventorySlot - AddItem - ������ �̸�: {0}", item.itemName);
    }
    public void RemoveItem(Item _item)
    {
        item = null;
        currentItemImage = null;
    }
    public string GetItemName()
    {
        return item.itemName;
    }
}
