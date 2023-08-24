using UnityEngine;
using static Define;

public class KillerRoomCabinetKiller : Trick
{
    [Header("열린 캐비넷")]
    public GameObject cabinetKillerOpen;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked(ItemName.ClosetKey))
            {
                Debug.LogFormat("{0} Solved", this.name);
                Inventory.Instance.RemoveItem(ItemName.ClosetKey);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
            }
        }
    }


    public override void SolvedAction()
    {
        cabinetKillerOpen.SetActive(true);
        gameObject.SetActive(false);
    }
}
