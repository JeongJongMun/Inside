using UnityEngine;
using UnityEngine.UI;

public class DrawerKid : NewTrick
{
#region Public Variables
    public Sprite drawerOpen;
#endregion

#region Protected Methods
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (_currentClickedItem.eItemType != Define.EItemType.DrawerKeyKid || IsComplete) 
        {
            Managers.Sound.Play("drawerLocked");
            return false;
        }
        Managers.Sound.Play("drawerOpened");
        NewInventory.instance.RemoveItem(_currentClickedItem);
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        Image image = GetComponent<Image>();
        image.sprite = drawerOpen;
        image.raycastTarget = false;
        transform.SetSiblingIndex(0);
    }
#endregion
}
