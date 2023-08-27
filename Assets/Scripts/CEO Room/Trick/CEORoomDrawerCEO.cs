using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CEORoomDrawerCEO : Trick
{
    [Header("비밀번호들")]
    public TMP_Text[] passwords;

    [Header("비밀번호 버튼")]
    public Image[] passwordButtons;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (passwords[0].text == "1" && passwords[1].text == "5" && passwords[2].text == "0")
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("electricDoorOpen");
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
        passwords[0].text = "1";
        passwords[1].text = "5";
        passwords[2].text = "0";
        foreach (Image button in passwordButtons)
        {
            button.raycastTarget = false;
        }
    }
}
