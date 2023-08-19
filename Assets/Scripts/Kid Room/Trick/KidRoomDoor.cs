using UnityEngine;

public class KidRoomDoor : Trick
{


    public override void TrySolve(GameObject obj)
    {
        if (obj.name == gameObject.name)
        {
            Debug.Log("문 잠김 사운드 재생");
        }
    }
    public override void SolvedAction()
    {
        throw new System.NotImplementedException();
    }
}
