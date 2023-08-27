using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CEORoomSafeCEO : Trick
{
    public TMP_Text[] passwords;
    public GameObject safeCEOZoomOpen;
    public GameObject latch4;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (passwords[0].text == "3" && passwords[1].text == "1" && passwords[2].text == "7" && passwords[3].text == "9")
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("lockerOpen");
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
        safeCEOZoomOpen.SetActive(true);
        latch4.SetActive(true);
    }
}
