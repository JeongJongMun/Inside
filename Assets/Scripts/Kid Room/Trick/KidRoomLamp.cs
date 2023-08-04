using UnityEngine;
using UnityEngine.UI;

public class KidRoomLamp : Trick
{
    public Sprite[] lamps; // 0 : lampOn, 1 : lampOff
    private bool isOn = true;
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (obj.name == "Lamp")
        {
            if (isOn)
            {
                Debug.Log("Lamp Off");
                gameObject.GetComponent<Image>().sprite = lamps[1];
                isOn = false;
            }
            else
            {
                Debug.Log("Lamp On");
                gameObject.GetComponent<Image>().sprite = lamps[0];
                isOn = true;
            }
        }
    }
}
