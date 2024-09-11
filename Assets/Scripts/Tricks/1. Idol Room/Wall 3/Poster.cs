public class Poster : NewTrick
{
#region Private Variables
    private HearingEventManager hearingEventManager;
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
        hearingEventManager = FindObjectOfType<HearingEventManager>();
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (_currentClickedItem.itemName != Define.ItemName.Cutter) return false;
        Managers.Sound.Play("cutter");
        hearingEventManager.OnHearingEvent(Define.RoomName.Idol);
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        gameObject.SetActive(false);
    }
#endregion
}