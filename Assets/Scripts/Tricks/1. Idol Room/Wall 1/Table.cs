using UnityEngine;

public class Table : NewTrick
{
#region Public Variables
    public GameObject musicBoxOnTable;
#endregion

#region Protected Methods
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (_currentClickedItem.eItemType != Define.EItemType.MusicBox || IsComplete) return false;
        NewInventory.instance.RemoveItem(_currentClickedItem);
        Managers.Sound.Play("bedFabric");
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        musicBoxOnTable.SetActive(true);
    }
#endregion
}