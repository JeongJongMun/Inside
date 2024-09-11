using UnityEngine;
using UnityEngine.UI;

public class FamilyPicture : NewTrick
{
#region Public Variables
    public Sprite familyPictureTorn;
#endregion

#region Protected Methods
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (_currentClickedItem.itemName != Define.ItemName.Cutter) return false;
        Managers.Sound.Play("cutter");
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        Image image = GetComponent<Image>();
        image.sprite = familyPictureTorn;
        image.raycastTarget = false;
    }
#endregion
}