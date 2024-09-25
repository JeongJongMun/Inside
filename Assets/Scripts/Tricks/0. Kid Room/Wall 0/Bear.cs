using UnityEngine;
using UnityEngine.UI;
/* Bear.cs
 * 0. Kid Room - Wall 0
 */
public class Bear : NewTrick
{
#region Private Variables
    private Image image;
    private HearingEventManager hearingEventManager;
#endregion

#region Public Variables
    public Sprite bearBody;
    public GameObject bearHead;
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
        image = GetComponent<Image>();
        hearingEventManager = FindObjectOfType<HearingEventManager>();
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (_currentClickedItem.eItemType != Define.EItemType.Cutter || IsComplete) return false;
        hearingEventManager.OnHearingEvent(Define.ERoomType.Kid);
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        image.sprite = bearBody;
        bearHead.SetActive(true);
    }
#endregion
}