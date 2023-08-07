using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemName
{
    None,

    // Kid
    Console,
    Cutter,
    KidRoomKey,
    Latch1,
    Lego1,
    Lego2,
    Lego3,
    Password,

    // Idol
    Broom,
    Latch2,
    MusicBox,
}

public class Item : MonoBehaviour
{
    [Header("��ü �̸�")]
    public string objectName;

    [Header("������ �̸�")]
    public ItemName itemName;

    [Header("������ �Ҽ� �� �̸�")]
    public string roomName;

    [Header("�κ��丮�� �ִ°�")]
    public bool isInInventory = false;

    private List<ItemName> need_other_item_to_acquired = new List<ItemName>()
    {
        ItemName.MusicBox,
    };

    private void Start()
    {
        objectName = gameObject.name;

        int index = objectName.IndexOf("(Clone)");
        if (index > 0) objectName = objectName.Substring(0, index);

        itemName = GetEnumFromName(objectName);

        // ���� �������� ���� �� �̸� ��������
        roomName = GameObject.FindWithTag("RoomManager").name.Substring(11);

        // �� �ε�ÿ� �������� ȹ���� ���� �ִٸ� �ı�
        if (DatabaseManager.Instance.IsItemAcquired(roomName, itemName) && !isInInventory)
        {
            Destroy(gameObject);
        }

        // ���� �������� ȹ���ϴµ� �ٸ� �������� �ʿ��Ѱ� �ƴ϶�� �� �ε�ÿ� OnClick ���� (���ڰ� �ִ� ��� ���� �� ���)
        if (!need_other_item_to_acquired.Contains(itemName))
        {
            Debug.LogFormat("{0} Item Added OnClickItem Listener", itemName);
            GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.OnClickItem(gameObject));
        }
    }
    // �̸��� Enum ������ ��ȯ�ϴ� �Լ�
    private ItemName GetEnumFromName(string name)
    {
        ItemName enumValue = ItemName.None;

        // Enum ������ ��ȸ�ϸ鼭 �̸��� ��ġ�ϴ� ���� ã��
        foreach (ItemName value in System.Enum.GetValues(typeof(ItemName)))
        {
            if (value.ToString() == name)
            {
                enumValue = value;
                break;
            }
        }

        return enumValue;
    }
}
