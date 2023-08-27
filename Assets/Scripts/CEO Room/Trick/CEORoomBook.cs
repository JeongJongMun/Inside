using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEORoomBook : Trick
{
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name && !IsSolved())
        {
            Debug.LogFormat("{0} Solved", this.name);
            SoundManager.instance.SFXPlay("bookSlide");
            SetIsSolved(true);
            SolvedAction();
        }
    }
    public override void SolvedAction()
    {
        transform.position += Vector3.right * 75;
    }
}
