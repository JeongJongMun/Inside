using UnityEngine;

public class KidRoomDoor : Trick
{
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == gameObject.name)
        {
            SoundManager.instance.SFXPlay("doorLocked");
            Debug.Log("�� ��� ���� ���");
        }
    }
    public override void SolvedAction()
    {
        throw new System.NotImplementedException();
    }
}
