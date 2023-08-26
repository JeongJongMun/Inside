using UnityEngine;
using UnityEngine.UI;

public class LivingRoomHatch : Trick
{
    // 정답 걸쇠 번호 : 0 6 9 15 -> 9 0 6 15
    //                 1 2 0 3
    [Header("정답 걸쇠")]
    public LatchHole[] latchHoles;

    [Header("해치")]
    public GameObject hatch;
    [Header("해치 열린거")]
    public GameObject hatchOpen;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            for (int i = 0; i < latchHoles.Length; i++)
            {
                if (latchHoles[i].GetInputLatchNumber() != i)
                {
                    Debug.LogFormat("{0} Not Solved", this.name);
                    return;
                }
            }
            Debug.LogFormat("{0} Solved", this.name);
            SoundManager.instance.SFXPlay("hatchOpen");
            SetIsSolved(true);
            SolvedAction();
        }
    }

    public override void SolvedAction()
    {
        hatch.SetActive(false);
        hatchOpen.SetActive(true);
    }
}
