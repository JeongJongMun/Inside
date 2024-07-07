using UnityEngine;
using UnityEngine.UI;
using static Define;

public class LivingRoomCardReader : Trick
{
    [Header("ī�� ������ ����")]
    public Image status;

    [Header("ī�� ������ ���� ����")]
    public Sprite statusGreen;

    [Header("������ �� ��")]
    public Image researcherDoor;

    [Header("������ �� �� ���� �̹���")]
    public Sprite researcherDoorOpen;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "CardReader")
        {
            if (Inventory.instance.IsClicked(ItemName.AccessCard))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("electricDoorOpen");
                SetIsSolved(true);
                SolvedAction();
                Inventory.instance.RemoveItem(ItemName.AccessCard);
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                // LivingRoom Manager ��ũ��Ʈ���� ȿ���� �߰������Ƿ� ���⼭�� �߰� �� ��
            }
        }
    }

    public override void SolvedAction()
    {
        status.sprite = statusGreen;
        researcherDoor.sprite = researcherDoorOpen;
    }
}
