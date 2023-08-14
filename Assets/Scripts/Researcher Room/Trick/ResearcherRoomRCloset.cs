using UnityEngine;

public class ResearcherRoomRCloset : Trick
{
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {

        }
    }

    public override void SolvedAction()
    {
        throw new System.NotImplementedException();
    }
}
