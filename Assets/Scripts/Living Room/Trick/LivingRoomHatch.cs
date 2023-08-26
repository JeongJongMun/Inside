using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LivingRoomHatch : Trick
{
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
        throw new System.NotImplementedException();
    }
}
