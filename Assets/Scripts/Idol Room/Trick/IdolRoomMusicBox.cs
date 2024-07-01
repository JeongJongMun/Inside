using UnityEngine;
using static Define;


public class IdolRoomMusicBox : Trick
{
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked(ItemName.Broom))
            {
                Debug.LogFormat("{0} Solved", this.name);
                Inventory.Instance.RemoveItem(ItemName.Broom);
                SetIsSolved(true);
                SolvedAction();
                InGameManager.Instance.OnClickItem(this.gameObject);

            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
            }
        }
    }


    public override void SolvedAction()
    {
        GameObject.FindWithTag("RoomManager").GetComponent<RoomManagerIdol>().RemoveTrick(this);
    }
}
