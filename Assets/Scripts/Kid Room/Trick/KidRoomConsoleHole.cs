using UnityEngine;
using UnityEngine.UI;

public class KidRoomConsoleHole : Trick
{
    public GameObject console;
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (obj.name == "ConsoleHole")
        {
            if (Inventory.Instance.IsClicked("Console"))
            {
                Debug.LogFormat("{0} Solved", obj.name);
                console.SetActive(true);
                gameObject.SetActive(false);
                Inventory.Instance.RemoveItem("Console");
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", obj.name);
            }
        }
    }
}
