using UnityEngine;
using UnityEngine.UI;
using static Define;

public class IdolRoomTable : Trick
{
    [Header("¿­¸° ¿À¸£°ñ")]
    public GameObject musicBoxOnTable;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked(ItemName.MusicBox))
            {
                Debug.LogFormat("{0} Solved", this.name);
                Inventory.Instance.RemoveItem(ItemName.MusicBox);
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
