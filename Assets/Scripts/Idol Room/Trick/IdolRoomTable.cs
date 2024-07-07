using UnityEngine;
using UnityEngine.UI;
using static Define;

public class IdolRoomTable : Trick
{
    [Header("���� ������")]
    public GameObject musicBoxOnTable;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.instance.IsClicked(ItemName.MusicBox))
            {
                Debug.LogFormat("{0} Solved", this.name);
                Inventory.instance.RemoveItem(ItemName.MusicBox);
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
        musicBoxOnTable.SetActive(true);
        GetComponent<Image>().raycastTarget = false;
    }
}
