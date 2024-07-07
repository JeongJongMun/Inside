using UnityEngine;
using UnityEngine.UI;
using static Define;

public class InventorySlot : MonoBehaviour
{
#region Private Variables
    private GameObject itemObject;
#endregion

#region Public Variables
    public Item item;
#endregion

#region Private Methods
    private void Awake()
    {
        itemObject = null;
        item = null;
    }
#endregion

#region Public Methods
    public void AddItem<T>(T _item)
    {
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
        itemObject = Instantiate(addedItem, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        itemObject.GetComponent<Item>().isInInventory = true;
        itemObject.GetComponent<Image>().raycastTarget = false;

        item = itemObject.GetComponent<Item>();

        int index = item.name.IndexOf("(Clone)");
        if (index > 0) item.name = item.name.Substring(0, index);

    }

    public void RemoveItem()
    {
        DatabaseManager.Instance.SetInventoryData(item.itemName, false);
        Destroy(itemObject);
        item = null;
    }
    public ItemName GetItemName() => item.itemName;
#endregion
}