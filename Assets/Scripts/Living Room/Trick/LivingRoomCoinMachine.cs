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
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                SoundManager.instance.SFXPlay("eletricFail");
                money.text = "0";
            }
        }
    }
    public override void SolvedAction()
    {
        CEODoor.sprite = CEODoorOpen;
        GetComponent<Button>().interactable = false;
    }
}
