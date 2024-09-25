using UnityEngine;
using static Define;

public class KillerRoomCabinetKiller : Trick
{
    [Header("���� ĳ���")]
    public GameObject cabinetKillerOpen;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.instance.IsClicked(EItemType.ClosetKey))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("lockerOpen");
                Inventory.instance.RemoveItem(EItemType.ClosetKey);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                SoundManager.instance.SFXPlay("drawerLocked");
            }
        }
    }


    public override void SolvedAction()
    {
        cabinetKillerOpen.SetActive(true);
        gameObject.SetActive(false);
    }
}
