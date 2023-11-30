using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class LivingRoomCoinMachine : Trick
{
    [Header("투입된 동전")]
    public TMP_Text money;

    [Header("CEO방 문")]
    public Image CEODoor;

    [Header("CEO방 문 열린 이미지")]
    public Sprite CEODoorOpen;

    [Header("버튼 그룹")]
    public Transform buttonGroup;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (money.text == "2200")
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("electricDoorOpen");
                Inventory.Instance.RemoveItem(ItemName.Coins);
                SetIsSolved(true);
                SolvedAction();
                DisableAllChildButtons();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                SoundManager.instance.SFXPlay("eletricFail");
            }
            money.text = "0";
        }
    }
    public override void SolvedAction()
    {
        CEODoor.sprite = CEODoorOpen;
        GetComponent<Button>().interactable = false;
    }

    void DisableAllChildButtons()
    {
        int childCount = buttonGroup.childCount;

        // 모든 자식 오브젝트의 버튼 컴포넌트 찾아서 interactable 속성을 false로 설정
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = buttonGroup.GetChild(i);
            Button buttonComponent = childTransform.GetComponent<Button>();

            if (buttonComponent != null) buttonComponent.interactable = false;
        }
    }
}
