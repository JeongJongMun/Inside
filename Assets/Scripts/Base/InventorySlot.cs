using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class InventorySlot : MonoBehaviour
{
    public GameObject itemObject; // 삭제를 위한 오브젝트 참조
    public Item item = null; // 획득한 아이템

    public void AddItem<T>(T _item)
    {
        // 에셋 로드
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
        // 인벤토리에 아이템 생성
        itemObject = Instantiate(addedItem, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        // 인벤토리에선 생성 시 삭제 X
        itemObject.GetComponent<Item>().isInInventory = true;
        // 인벤토리에서 클릭 X
        itemObject.GetComponent<Image>().raycastTarget = false;

        item = itemObject.GetComponent<Item>();

        // (Clone) 지우기
        int index = item.name.IndexOf("(Clone)");
        if (index > 0) item.name = item.name.Substring(0, index);

    }

    public void RemoveItem()
    {
        Debug.LogFormat("{0} 아이템 삭제", item.itemName);
        DatabaseManager.Instance.SetInventoryData(item.itemName, false);
        Destroy(itemObject);
        item = null;
    }
    public ItemName GetItemName()
    {
        return item.itemName;
    }
}
