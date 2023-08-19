using TMPro;
using UnityEngine;

public class LivingRoomCoinMachine : Trick
{
    public TMP_Text money;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (money.text == "2200")
            {
                Debug.LogFormat("{0} Solved", this.name);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                money.text = "0";
            }
        }
    }
    public override void SolvedAction()
    {
        Debug.Log("CEO规 巩 凯府绰 家府 犁积");
    }
}
