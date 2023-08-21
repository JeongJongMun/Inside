using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CEORoomDrawerCEO : Trick
{
    public TMP_Text[] passwords;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (passwords[0].text == "1" && passwords[1].text == "5" && passwords[2].text == "0")
            {
                Debug.LogFormat("{0} Solved", this.name);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
            }
        }
    }
    public override void SolvedAction()
    {
        throw new System.NotImplementedException();
    }
}
