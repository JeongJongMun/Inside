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
            if (Inventory.Instance.IsClicked(ItemName.AccessCard))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SetIsSolved(true);
                SolvedAction();
                Inventory.Instance.RemoveItem(ItemName.AccessCard);
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
            }
        }
    }

    public override void SolvedAction()
    {
        status.sprite = statusGreen;
        researcherDoor.sprite = researcherDoorOpen;
    }
}