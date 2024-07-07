using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public enum Location {
    Field,
    Inventory,
}
public class Item : MonoBehaviour
{
#region Private Variables
#endregion

#region Public Variables
    public ItemName itemName;
    public Location itemType;
#endregion

#region Private Methods
#endregion

#region Public Methods

#endregion


    public bool isInInventory = false;

    private List<ItemName> need_other_item_to_acquired = new List<ItemName>()
    {
        ItemName.MusicBox,
    };

    private void Start()
    {
        string objectName = gameObject.name;
        int index = objectName.IndexOf("(Clone)");
        if (index > 0) objectName = objectName.Substring(0, index);
        itemName = GetEnumFromName<ItemName>(objectName);

        if (!isInInventory && DatabaseManager.Instance.GetData(itemName))
        {
            Destroy(gameObject);
        }

        // if (!need_other_item_to_acquired.Contains(itemName))
        // {
        //     GetComponent<Button>().onClick.AddListener(() => InGameManager.Instance.OnClickItem(gameObject));
        // }
    }

    static public T GetEnumFromName<T>(string name) where T : Enum
    {
        foreach (T value in Enum.GetValues(typeof(T)))
        {
            if (value.ToString() == name)
            {
                return value;
            }
        }

        return default;
    }
}
