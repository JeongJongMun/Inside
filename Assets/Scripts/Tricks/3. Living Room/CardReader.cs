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
        if (_currentClickedItem.itemName != Define.ItemName.AccessCard || IsComplete) {
            GameManager.instance.soundManager.Play("electricFail");
            return false;
        } 

        NewInventory.instance.RemoveItem(_currentClickedItem);
        GameManager.instance.soundManager.Play("electricDoorOpen");
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