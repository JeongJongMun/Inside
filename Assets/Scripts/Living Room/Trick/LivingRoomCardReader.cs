using UnityEngine;
using UnityEngine.UI;
using static Define;

public class LivingRoomCardReader : Trick
{
    [Header("카드 리더기 상태")]
    public Image status;

    [Header("카드 리더기 열린 상태")]
    public Sprite statusGreen;

    [Header("연구원 방 문")]
    public Image researcherDoor;

    [Header("연구원 방 문 열린 이미지")]
    public Sprite researcherDoorOpen;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "CardReader")
        {
            if (Inventory.Instance.IsClicked(ItemName.AccessCard))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("electricDoorOpen");
                SetIsSolved(true);
                SolvedAction();
                Inventory.Instance.RemoveItem(ItemName.AccessCard);
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                // LivingRoom Manager 스크립트에서 효과음 추가했으므로 여기서는 추가 안 함
            }
        }
    }

    public override void SolvedAction()
    {
        status.sprite = statusGreen;
        researcherDoor.sprite = researcherDoorOpen;
    }
}
