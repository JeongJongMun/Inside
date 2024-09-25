using UnityEngine;
using UnityEngine.UI;
using static Define;

public class CEORoomDeer : Trick
{
    [Header("�� ���� �罿 Ȯ��X")]
    public Sprite shotDeer;

    [Header("�� ���� �罿 Ȯ��")]
    public Sprite shotDeerZoom;

    [Header("�罿 Ȯ��")]
    public Image deerZoom;

    [Header("���� Ȯ�� ��ư")]
    public GameObject questionButton;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.instance.IsClicked(EItemType.Gun) && !IsSolved())
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("gunshot");
                SetIsSolved(true);
                SolvedAction();
                // VoiceManager.instance.ScreamingMode(RoomName.CEO);
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
