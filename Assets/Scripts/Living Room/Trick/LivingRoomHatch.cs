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

        }
    }

    public override void SolvedAction()
    {
        throw new System.NotImplementedException();
    }
}
