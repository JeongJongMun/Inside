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

    [Header("��ư �׷�")]
    public Transform buttonGroup;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (money.text == "2200")
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("electricDoorOpen");
                Inventory.instance.RemoveItem(ItemName.Coins);
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

        // ��� �ڽ� ������Ʈ�� ��ư ������Ʈ ã�Ƽ� interactable �Ӽ��� false�� ����
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = buttonGroup.GetChild(i);
            Button buttonComponent = childTransform.GetComponent<Button>();

            if (buttonComponent != null) buttonComponent.interactable = false;
        }
    }
}
