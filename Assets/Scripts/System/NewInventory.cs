using System;
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
    private GameManager gameManager;
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
    private void Start()
    {
        gameManager = GameManager.instance;
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
                Managers.Sound.Play("bedFabric");
                return true;
            }
        }
        return false;
    }
    public bool RemoveItem(NewItem _item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == _item) {
                items[i] = null;
                icons[i].RemoveIcon();
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
    public NewItem GetClickedItem()
    {
        for (int i = 0; i < toggles.Length; i++) {
            if (toggles[i].isOn) {
                toggles[i].isOn = false;
                return items[i];
            }
        }
        GameObject emptyObject = new GameObject();
        NewItem emptyItem = emptyObject.AddComponent<NewItem>();
        emptyItem.eItemType = Define.EItemType.None;
        Destroy(emptyObject, 0.5f);
        return emptyItem;
    }
    public NewItem GetItem(Define.EItemType eItemType)
    {
        for (int i = 0; i < items.Length; i++) {
            if (items[i] != null && items[i].eItemType == eItemType) {
                return items[i];
            }
        }
        return null;
    }
#endregion
}