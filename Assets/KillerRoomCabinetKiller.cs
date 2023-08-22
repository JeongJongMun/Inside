using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerRoomCabinetKiller : Trick
{
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked(Define.ItemName.ClosetKey))
            {
                Debug.LogFormat("{0} Solved", this.name);
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

    }
}
