using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ClosetResearcher : NewTrick
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    public GameObject closetOpen;
    
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    public bool isStandSolved = false;
    
    // --------------------------------------------------
    // Functions
    // --------------------------------------------------
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (!Managers.Trick.IsComplete(Define.ETrickType.Stand))
        {
            Managers.Sound.Play("doorLocked");
            return false;
        }
        Managers.Sound.Play("closet");
        // VoiceManager.instance.ScreamingMode(Define.RoomName.Researcher);

        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        closetOpen.GetComponent<Image>().enabled = true;
    }
}
