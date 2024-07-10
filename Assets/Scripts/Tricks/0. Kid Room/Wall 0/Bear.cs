using UnityEngine;
using UnityEngine.UI;
/* Bear.cs
 * 0. Kid Room - Wall 0
 */
public class Bear : Observer
{
#region Private Variables
    private Image image;
#endregion

#region Public Variables
    public Sprite bearBody;
    public GameObject bearHead;
    public GameObject password;
#endregion

#region Private Methods
#endregion

#region Public Methods
    public override void OnNotify(Define.TrickName _trickName)
    {
        if (_trickName != this.trickName) {
            return;
        }
        if (NewInventory.instance.GetClickedItemName() != Define.ItemName.Cutter && !IsComplete) {
            return;
        }
        // VoiceManager.Instance.ScreamingMode(Define.RoomName.Kid);
        IsComplete = true;

        image.sprite = bearBody;
        image.raycastTarget = false;
        bearHead.SetActive(true);
        password.SetActive(true);
    }
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
        image = GetComponent<Image>();
    }
#endregion
}