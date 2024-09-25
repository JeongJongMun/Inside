using UnityEngine;
using UnityEngine.UI;

public class CardReader : NewTrick
{
#region Private Variables
#endregion

#region Public Variables
    public Image status;
    public Sprite statusGreen;
    public Image researcherDoor;
    public Sprite researcherDoorOpen;
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (_currentClickedItem.eItemType != Define.EItemType.AccessCard || IsComplete) {
            Managers.Sound.Play("electricFail");
            return false;
        } 

        NewInventory.instance.RemoveItem(_currentClickedItem);
        Managers.Sound.Play("electricDoorOpen");
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        status.sprite = statusGreen;
        researcherDoor.sprite = researcherDoorOpen;
    }
#endregion
}