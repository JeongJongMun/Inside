using UnityEngine;
using UnityEngine.UI;

public class KidRoomCurtain : Trick
{
    public Sprite[] curtains;
    private bool isOpened = false;
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (obj.name == "Curtain")
        {
            if (!isOpened)
            {
                Debug.Log("Curtain Opened");
                gameObject.GetComponent<Image>().sprite = curtains[1];
                isOpened = true;
            }
            else
            {
                Debug.Log("Curtain Closed");
                gameObject.GetComponent<Image>().sprite = curtains[0];
                isOpened = false;
            }
        }

    }
}
