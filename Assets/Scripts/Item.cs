using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemSprite;

    private void Start()
    {
        itemName = transform.name;
        itemSprite = GetComponent<Image>().sprite;
        Debug.LogFormat("Item : {0}", itemName);
    }
}
