using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item = null; // »πµÊ«— æ∆¿Ã≈€

    public void AddItem(Item _item)
    {
        GameObject addedItem = Resources.Load<GameObject>("Prefabs/Items/" +  _item.itemName);
        item = Instantiate(addedItem, gameObject.transform.position, Quaternion.identity, gameObject.transform).GetComponent<Item>();

        // (Clone) ¡ˆøÏ±‚
        int index = item.name.IndexOf("(Clone)");
        if (index > 0) item.name = item.name.Substring(0, index);


        Debug.LogFormat("{0} added in slot", item.itemName);
    }
    public void RemoveItem()
    {
        item = null;
    }
    public string GetItemName()
    {
        return item.itemName;
    }
}
