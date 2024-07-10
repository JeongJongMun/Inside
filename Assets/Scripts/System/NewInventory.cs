using UnityEngine;
using UnityEngine.UI;
/* Inventory.cs
 * 인벤토리를 관리하는 스크립트
 * 함수 목록
 * AddItem(NewItem _item) : 아이템 추가
 * RemoveItem(Item _item) : 아이템 제거
 * ClearInventory() : 인벤토리 초기화
 * GetClickedItem() : 현재 클릭된 아이템 확인
 * SortInventory() : 인벤토리 정렬
 */
public class NewInventory : MonoBehaviour    
{
#region Private Variables
    [SerializeField] private NewItem[] items;
    private Toggle[] toggles;
    private NewInventorySlot[] icons;
#endregion

#region Public Variables
    public static NewInventory instance = null;
#endregion

#region Private Methods
    private void Awake()
    {
        instance = this;

        int childCount = this.transform.childCount;

        items = new NewItem[childCount];
        toggles = new Toggle[childCount];
        icons = new NewInventorySlot[childCount];
        for (int i = 0; i < childCount; i++) {
            items[i] = null;
            Transform child = this.transform.GetChild(i);
            toggles[i] = child.GetComponent<Toggle>();
            icons[i] = child.GetComponent<NewInventorySlot>();
        }
    }
#endregion

#region Public Methods
    public bool AddItem(NewItem _item)
    {
        for (int i = 0; i < items.Length; i++) {
            if (items[i] == null) {
                items[i] = _item;
                icons[i].SetIcon(_item.icon);
                _item.gameObject.SetActive(false);
                return true;
            }
        }
        return false;
    }
    public bool RemoveItem(Item _item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == _item) {
                items[i] = null;
                return true;
            }
        }
        return false;
    }
    public void ClearInventory()
    {
        for (int i = 0; i < items.Length; i++) {
            items[i] = null;
        }
    }
    public Define.ItemName GetClickedItemName()
    {
        for (int i = 0; i < toggles.Length; i++) {
            if (toggles[i].isOn) {
                return items[i].itemName;
            }
        }
        return Define.ItemName.None;
    }
#endregion
}