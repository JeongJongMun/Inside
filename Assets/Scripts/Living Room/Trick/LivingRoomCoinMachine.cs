using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class LivingRoomCoinMachine : Trick
{
    [Header("���Ե� ����")]
    public TMP_Text money;

    [Header("CEO�� ��")]
    public Image CEODoor;

    [Header("CEO�� �� ���� �̹���")]
    public Sprite CEODoorOpen;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (money.text == "2200")
            {
                Debug.LogFormat("{0} Solved", this.name);
                Inventory.Instance.RemoveItem(ItemName.Coins);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                money.text = "0";
            }
        }
    }
    public override void SolvedAction()
    {
        Debug.Log("CEO�� �� ������ �Ҹ� ���");
        CEODoor.sprite = CEODoorOpen;
    }
}
