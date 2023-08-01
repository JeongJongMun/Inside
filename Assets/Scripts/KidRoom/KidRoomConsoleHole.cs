using UnityEngine;
using UnityEngine.UI;

public class KidRoomConsoleHole : Trick
{
    public Sprite console;
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (obj.name == "ConsoleHole")
        {
            if (Inventory.Instance.IsClicked("Console"))
            {
                Debug.LogFormat("{0} Solved", obj.name);

                gameObject.GetComponent<Image>().sprite = console;
            }
        }
    }
}
