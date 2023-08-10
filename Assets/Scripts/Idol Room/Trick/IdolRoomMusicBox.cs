using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class IdolRoomMusicBox : Trick
{
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked("Broom"))
            {
                Debug.LogFormat("{0} Solved", this.name);
                GameManager.Instance.OnClickItem(this.gameObject);
                SetIsSolved(true);
                SolvedAction();
                GameObject.FindWithTag("RoomManager").GetComponent<RoomManagerIdol>().RemoveTrick(this);
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
            }
        }
    }


    public override void SolvedAction()
    {

    }
}
