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
    // ���� ����: ���� ��ü�� ���� �� �޸𸮸� ����Ű�°� �ƴ϶� ������ �� �޸𸮸� ����Ű�� ��
    public Item DeepCopy()
    {
        Item newCopy = this.gameObject.AddComponent<Item>();
        newCopy.itemName = this.itemName;
        newCopy.itemSprite = this.itemSprite;
        return newCopy;
    }

}
