using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class Item : MonoBehaviour
{
    [Header("열거형 아이템 이름")]
    public ItemName itemName;

    [Header("인벤토리에 있는가")]
    public bool isInInventory = false;

    private List<ItemName> need_other_item_to_acquired = new List<ItemName>()
    {
        ItemName.MusicBox,
    };

    private void Start()
    {
        // 현재 객체의 이름 및 아이템 열거형 가져오기
        string objectName = gameObject.name;
        int index = objectName.IndexOf("(Clone)");
        if (index > 0) objectName = objectName.Substring(0, index);
        itemName = GetEnumFromName<ItemName>(objectName);

        // 씬 로드시에 아이템을 획득한 적이 있다면 파괴
        if (DatabaseManager.Instance.IsItemAcquired(itemName) && !isInInventory)
        {
            Destroy(gameObject);
        }

        // 현재 아이템을 획득하는데 다른 아이템이 필요한게 아니라면 씬 로드시에 OnClick 적용 (인자가 있는 경우 람다 식 사용)
        if (!need_other_item_to_acquired.Contains(itemName))
        {
            GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.OnClickItem(gameObject));
        }
    }

    // 이름을 Enum 값으로 변환하는 함수
    static public T GetEnumFromName<T>(string name) where T : Enum
    {
        // Enum 값들을 순회하면서 이름과 일치하는 값을 찾음
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
