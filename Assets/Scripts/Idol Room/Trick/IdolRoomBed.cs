using UnityEngine;
using UnityEngine.UI;

public class IdolRoomBed : Trick
{
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            SoundManager.instance.SFXPlay("bedFabric");
            Debug.LogFormat("{0} Solved", this.name);
            SetIsSolved(true);
            SolvedAction();
        }
    }
    public override void SolvedAction()
    {
        this.gameObject.SetActive(false);
    }
}
