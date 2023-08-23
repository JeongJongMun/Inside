using UnityEngine;
using UnityEngine.UI;

public class LivingRoomHatch : Trick
{
    // ���� �ɼ� ��ȣ : 0 6 9 15
    [Header("���� �ɼ�")]
    public LatchHole[] latchHoles;

    [Header("��ġ")]
    public GameObject hatch;
    [Header("��ġ ������")]
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