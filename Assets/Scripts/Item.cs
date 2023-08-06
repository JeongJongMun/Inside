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
}

public class Item : MonoBehaviour
{
    public string objectName;
    public ItemName itemName;
    public string roomName;
    public bool isInInventory = false;

    private void Start()
    {
        objectName = gameObject.name;

        int index = objectName.IndexOf("(Clone)");
        if (index > 0) objectName = objectName.Substring(0, index);

        itemName = GetEnumFromName(objectName);

        // ���� �������� ���� �� �̸� ��������
        roomName = GameObject.FindWithTag("RoomManager").name.Substring(11);

        // �� �ε�ÿ� �������� ȹ���� ���� �ִٸ� �ı�
        if (GameManager.Instance.IsItemAcquired(roomName, itemName) && !isInInventory)
        {
            Destroy(gameObject);
        }

        // �� �ε�ÿ� OnClick ���� (���ڰ� �ִ� ��� ���� �� ���)
        GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.OnClickItem(gameObject));
    }
    // Enum ���� �̸����� ��ȯ�ϴ� �Լ�
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
