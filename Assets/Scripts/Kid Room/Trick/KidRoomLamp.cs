using UnityEngine;
using UnityEngine.UI;

public class KidRoomLamp : Trick
{

    public Sprite[] lamps; // 0 : lampOn, 1 : lampOff

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "LampZoom" || obj.name == "Lamp")
        {
            if (!isSolved)
            {
                Debug.Log("Lamp Off");
                SolvedAction();
            }
            else
            {
                Debug.Log("Lamp On");
                gameObject.GetComponent<Image>().sprite = lamps[0];
                isSolved = false;
            }
        }
    }
    public override void SolvedAction()
    {
        gameObject.GetComponent<Image>().sprite = lamps[1];
        isSolved = true;
    }
}
