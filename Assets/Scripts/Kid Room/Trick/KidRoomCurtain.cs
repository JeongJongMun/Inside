using UnityEngine;
using UnityEngine.UI;

public class KidRoomCurtain : Trick
{
    public Sprite[] curtains;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "Curtain")
        {
            if (!isSolved)
            {
                Debug.Log("Curtain Opened");
                SolvedAction();
            }
            else
            {
                Debug.Log("Curtain Closed");
                gameObject.GetComponent<Image>().sprite = curtains[0];
                isSolved = false;
            }
        }
    }
    public override void SolvedAction()
    {
        gameObject.GetComponent<Image>().sprite = curtains[1];
        isSolved = true;
    }
}
