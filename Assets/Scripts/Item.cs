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
    // 깊은 복사: 기존 개체와 같은 힙 메모리를 가리키는게 아니라 독립된 힙 메모리를 가리키게 함
    public Item DeepCopy()
    {
        Item newCopy = this.gameObject.AddComponent<Item>();
        newCopy.itemName = this.itemName;
        newCopy.itemSprite = this.itemSprite;
        return newCopy;
    }

}
