using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;

    private void Awake()
    {
        itemName = gameObject.name;

        int index = itemName.IndexOf("(Clone)");
        if (index > 0) itemName = itemName.Substring(0, index);
    }
}
