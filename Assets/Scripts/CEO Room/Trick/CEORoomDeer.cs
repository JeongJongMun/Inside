using UnityEngine;
using UnityEngine.UI;
using static Define;

public class CEORoomDeer : Trick
{
    [Header("ÃÑ ¸ÂÀº »ç½¿ È®´ëX")]
    public Sprite shotDeer;

    [Header("ÃÑ ¸ÂÀº »ç½¿ È®´ë")]
    public Sprite shotDeerZoom;

    [Header("»ç½¿ È®´ë")]
    public Image deerZoom;

    [Header("¹®Á¦ È®´ë ¹öÆ°")]
    public GameObject questionButton;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked(ItemName.Gun) && !IsSolved())
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("gunshot");
                SetIsSolved(true);
                SolvedAction();
                VoiceManager.Instance.ScreamingMode(RoomName.CEO);
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
            }
        }
    }
    public override void SolvedAction()
    {
        GetComponent<Image>().sprite = shotDeer;
        deerZoom.sprite = shotDeerZoom;
        deerZoom.GetComponent<Image>().raycastTarget = false;
        questionButton.SetActive(true);
    }
}
