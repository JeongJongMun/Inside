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

        // 현재 아이템이 속한 방 이름 가져오기
        roomName = GameObject.FindWithTag("RoomManager").name.Substring(11);

        // 씬 로드시에 아이템을 획득한 적이 있다면 파괴
        if (GameManager.Instance.IsItemAcquired(roomName, itemName) && !isInInventory)
        {
            Destroy(gameObject);
        }

        // 씬 로드시에 OnClick 적용 (인자가 있는 경우 람다 식 사용)
        GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.OnClickItem(gameObject));
    }
    // Enum 값을 이름으로 변환하는 함수
    private ItemName GetEnumFromName(string name)
    {
        ItemName enumValue = ItemName.None;

        // Enum 값들을 순회하면서 이름과 일치하는 값을 찾음
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
