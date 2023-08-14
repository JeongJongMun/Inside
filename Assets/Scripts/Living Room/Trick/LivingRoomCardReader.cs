using UnityEngine;
using UnityEngine.UI;
using static Define;

public class LivingRoomCardReader : Trick
{
    [Header("카드 리더기 상태")]
    public Image status;

    [Header("카드 리더기 열린 상태")]
    public Sprite statusGreen;

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
        Debug.Log("연구원 방 문 열림");
        status.sprite = statusGreen;
    }
}
