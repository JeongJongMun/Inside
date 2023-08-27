using UnityEngine;
using static Define;

public class IdolRoomPoster : Trick
{

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked(ItemName.Cutter))
            {
                Debug.LogFormat("{0} Solved", this.name);
                VoiceManager.Instance.ScreamingMode(RoomName.Idol);
                SetIsSolved(true);
                SolvedAction();
            }
        }
    }
    public override void SolvedAction()
    {
        gameObject.SetActive(false);
    }
}
