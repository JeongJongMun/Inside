using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class Item : MonoBehaviour
{
    [Header("������ ������ �̸�")]
    public ItemName itemName;

    [Header("�κ��丮�� �ִ°�")]
    public bool isInInventory = false;

    private List<ItemName> need_other_item_to_acquired = new List<ItemName>()
    {
        ItemName.MusicBox,
    };

    private void Start()
    {
        // ���� ��ü�� �̸� �� ������ ������ ��������
        string objectName = gameObject.name;
        int index = objectName.IndexOf("(Clone)");
        if (index > 0) objectName = objectName.Substring(0, index);
        itemName = GetEnumFromName<ItemName>(objectName);

        // �� �ε�ÿ� �������� ȹ���� ���� �ִٸ� �ı�
        if (DatabaseManager.Instance.IsItemAcquired(itemName) && !isInInventory)
        {
            Destroy(gameObject);
        }

        // ���� �������� ȹ���ϴµ� �ٸ� �������� �ʿ��Ѱ� �ƴ϶�� �� �ε�ÿ� OnClick ���� (���ڰ� �ִ� ��� ���� �� ���)
        if (!need_other_item_to_acquired.Contains(itemName))
        {
            GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.OnClickItem(gameObject));
        }
    }

    // �̸��� Enum ������ ��ȯ�ϴ� �Լ�
    static public T GetEnumFromName<T>(string name) where T : Enum
    {
        // Enum ������ ��ȸ�ϸ鼭 �̸��� ��ġ�ϴ� ���� ã��
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
