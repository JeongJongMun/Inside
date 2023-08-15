using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IdolRoomLocker : Trick
{
    [Header("금고 비밀번호")]
    public TMP_Text[] P;

    [Header("열린 금고 오브젝트")]
    public GameObject lockerOpen;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (P[0].text + P[1].text + P[2].text + P[3].text == "1004")
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
        lockerOpen.SetActive(true);
        gameObject.SetActive(false);
        //Color color = gameObject.GetComponent<Image>().color;
        //color.a = 0;
        //gameObject.GetComponent<Image>().color = color;
        //gameObject.GetComponent<Image>().raycastTarget = false;
    }
    public void OnClickPassword(TMP_Text tmp_text)
    {
        int number = int.Parse(tmp_text.text);
        number = (number + 1) % 10;
        tmp_text.text = number.ToString();
        TrySolve(this.gameObject);
    }
}
