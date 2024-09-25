using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
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
    [FormerlySerializedAs("itemName")] public EItemType eItemType;
    public Location itemType;
#endregion

#region Private Methods
#endregion

#region Public Methods

#endregion


    public bool isInInventory = false;

    private List<EItemType> need_other_item_to_acquired = new List<EItemType>()
    {
        EItemType.MusicBox,
    };

    private void Start()
    {
        string objectName = gameObject.name;
        int index = objectName.IndexOf("(Clone)");
        if (index > 0) objectName = objectName.Substring(0, index);
        eItemType = GetEnumFromName<EItemType>(objectName);

        if (!isInInventory && DatabaseManager.Instance.GetData(eItemType))
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
