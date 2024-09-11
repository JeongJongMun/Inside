using UnityEngine;
using UnityEngine.UI;

public class ClosetIdol : NewTrick
{
#region Public Variables
    public Sprite closetOpen;
#endregion

#region Protected Methods
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        Managers.Sound.Play("closet");
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        Image image = GetComponent<Image>();
        image.sprite = closetOpen;
        transform.parent.SetSiblingIndex(0);
    }
#endregion
}