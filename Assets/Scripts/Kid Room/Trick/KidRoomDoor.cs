using UnityEngine;

public class KidRoomDoor : Trick
{
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == gameObject.name)
        {
            throw new System.NotImplementedException();
        }
    }
    public override void SolvedAction()
    {
        throw new System.NotImplementedException();
    }
}
