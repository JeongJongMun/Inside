using UnityEngine;

public class KidRoomDoor : Trick
{
    public AudioClip doorClosedClip;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == gameObject.name)
        {
            SoundManager.instance.SFXPlay("DoorClosed", doorClosedClip);
            Debug.Log("�� ��� ���� ���");
        }
    }
    public override void SolvedAction()
    {
        throw new System.NotImplementedException();
    }
}
