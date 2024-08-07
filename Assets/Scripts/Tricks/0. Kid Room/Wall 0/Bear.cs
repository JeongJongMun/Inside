using UnityEngine;
using UnityEngine.UI;
/* Bear.cs
 * 0. Kid Room - Wall 0
 */
public class Bear : NewTrick
{
#region Private Variables
    private Image image;
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
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (_currentClickedItem.itemName != Define.ItemName.Cutter || IsComplete) return false;
        // VoiceManager.Instance.ScreamingMode(Define.RoomName.Kid);
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