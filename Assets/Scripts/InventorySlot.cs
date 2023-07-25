using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item = null; // 획득한 아이템
    public Image itemImage;  // 아이템의 이미지


    // 아이템 이미지의 투명도 조절
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
        Debug.LogFormat("InventorySlot - AddItem - 아이템 이름: {0}", item.itemName);
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
