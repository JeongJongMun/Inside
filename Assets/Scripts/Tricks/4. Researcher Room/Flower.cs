using UnityEngine;
using UnityEngine.UI;

public class Flower : MonoBehaviour
{
#region Private Variables
    private RoomManager roomManager;
    private NewInventory inventory;
#endregion

#region public Variables
    public GameObject flowerZoom;
#endregion

#region Private Methods
    private void Start() 
    {
        roomManager = FindObjectOfType<RoomManager>();
        inventory = NewInventory.instance;
        if (TryGetComponent<Button>(out var button)) {
            button.onClick.AddListener(OnClick);
        }
    }
    private void OnClick()
    {
        if (inventory.GetClickedItem().eItemType != Define.EItemType.Magnifier) return;
        roomManager.ZoomIn(flowerZoom);
    }
#endregion
}