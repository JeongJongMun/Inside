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
        if (_currentClickedItem.eItemType != Define.EItemType.Cutter) return false;
        Managers.Sound.Play("cutter");
        hearingEventManager.OnHearingEvent(Define.ERoomType.Idol);
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        gameObject.SetActive(false);
    }
#endregion
}