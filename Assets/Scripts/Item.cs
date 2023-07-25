using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemName;
    public Image itemImage;

    private void Start()
    {
        itemName = transform.name;
        itemImage = GetComponent<Image>();
        Debug.LogFormat("Item : {0}", itemName);
    }
}
