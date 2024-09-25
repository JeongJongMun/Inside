public class MusicBox : NewTrick
{
#region Protected Methods
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (_currentClickedItem.eItemType != Define.EItemType.Broom || IsComplete) return false;
        NewInventory.instance.RemoveItem(_currentClickedItem);

        NewItem item = this.gameObject.AddComponent<NewItem>();
        item.InitializeItem();
        NewInventory.instance.AddItem(item);

        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        gameObject.SetActive(false);
    }
#endregion
}