using UnityEngine;
using UnityEngine.UI;
public class NewInventory : MonoBehaviour    
{
#region Private Variables
    private NewItem[] items;
    private int maxCapacity = 10;
    private ToggleGroup toggleGroup;
    private GameObject[] slots;
#endregion

#region Public Variables
    public NewItem[] Items { get { return items; } }
    public int MaxCapacity { get { return maxCapacity; } }
    public static NewInventory instance = null;
    public GameObject slotHolder;
#endregion

#region Private Methods
    private void Awake()
    {
        instance = this;
        toggleGroup = GetComponentInChildren<ToggleGroup>();

        items = new NewItem[maxCapacity];
        for (int i = 0; i < items.Length; i++) {
            items[i] = null;
        }
        
        int totalSlots = slotHolder.transform.childCount;
        slots = new GameObject[totalSlots];
        for (int i = 0; i < totalSlots; i++) {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
    }
#endregion

#region Public Methods
    public bool AddItem(NewItem _item)
    {
        for (int i = 0; i < items.Length; i++) {
            if (items[i] == null) {
                items[i] = _item;
                
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
#endregion
}