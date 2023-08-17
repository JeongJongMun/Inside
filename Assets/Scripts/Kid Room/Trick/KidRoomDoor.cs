using UnityEngine;

public class KidRoomDoor : Trick
{
    public AudioClip doorClip;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == gameObject.name)
        {
            SoundManager.instance.SFXPlay("Door", doorClip);
            Debug.Log("ï¿½ï¿½ ï¿½ï¿½ï¿? ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿?");
        }
    }
    public override void SolvedAction()
    {
        throw new System.NotImplementedException();
    }
}
