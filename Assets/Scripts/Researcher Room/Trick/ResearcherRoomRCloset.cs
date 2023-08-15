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
                SetIsSolved(true);
                SolvedAction();
                VoiceManager.Instance.ScreamingMode();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
            }
        }
    }

    public override void SolvedAction()
    {
        clothes.SetActive(true);
    }
}
