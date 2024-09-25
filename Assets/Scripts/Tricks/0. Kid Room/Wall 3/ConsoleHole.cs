using UnityEngine;
using UnityEngine.UI;

public class ConsoleHole : NewTrick
{
#region Private Variables
#endregion

#region Public Variables
#endregion

#region Private Methods
#endregion

#region Public Methods
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (_currentClickedItem.eItemType != Define.EItemType.Console || IsComplete) return false;
        Managers.Sound.Play("consoleInsert");
        NewInventory.instance.RemoveItem(_currentClickedItem);
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        gameObject.SetActive(false);
    }
#endregion
}