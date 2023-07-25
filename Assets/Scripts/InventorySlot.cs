using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item = null; // ȹ���� ������
    public Image itemImage;  // �������� �̹���


    // ������ �̹����� ���� ����
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddItem(Item _item)
    {
        item = _item;
        itemImage.sprite = item.itemSprite;
        SetColor(1);
        Debug.LogFormat("InventorySlot - AddItem - ������ �̸�: {0}", item.itemName);
    }
    public void RemoveItem()
    {
        item = null;
        itemImage.sprite = null;
        SetColor(0);
    }
    public string GetItemName()
    {
        return item.itemName;
    }
}
