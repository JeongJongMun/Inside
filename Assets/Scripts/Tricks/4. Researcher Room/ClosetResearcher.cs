using UnityEngine;

public class ClosetResearcher : Trick
{
    public bool isStandSolved = false;
    public GameObject clothes;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (isStandSolved)
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("closet");
                SetIsSolved(true);
                SolvedAction();
                // VoiceManager.instance.ScreamingMode(Define.RoomName.Researcher);
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                SoundManager.instance.SFXPlay("doorLocked");
            }
        }
    }

    public override void SolvedAction()
    {
        clothes.SetActive(true);
    }
}
