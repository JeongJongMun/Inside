using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item = null; // 획득한 아이템
    public Image currentItemImage = null; // 현재 획득한 아이템의 이미지

    private void Start()
    {
        currentItemImage = this.transform.GetChild(2).transform.GetComponent<Image>();
    }

    public void AddItem(Item _item)
    {
        item = _item;
        currentItemImage = item.itemImage;
        Debug.LogFormat("InventorySlot - AddItem - 아이템 이름: {0}", item.itemName);
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
