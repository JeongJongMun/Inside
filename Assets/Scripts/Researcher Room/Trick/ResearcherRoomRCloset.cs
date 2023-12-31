using UnityEngine;

public class ResearcherRoomRCloset : Trick
{
    [Header("Stand 트릭이 해결 되었는가")]
    public bool isStandSolved = false;

    [Header("Clothes")]
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
                VoiceManager.Instance.ScreamingMode(Define.RoomName.Researcher);
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
