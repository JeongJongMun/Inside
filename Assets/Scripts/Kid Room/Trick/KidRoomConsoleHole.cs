using UnityEngine;
using UnityEngine.UI;
using static Define;

public class KidRoomConsoleHole : Trick
{

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "ConsoleHole")
        {
            if (Inventory.Instance.IsClicked(ItemName.Console))
            {
                Debug.LogFormat("{0} Solved", obj.name);
                Inventory.Instance.RemoveItem(ItemName.Console);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", obj.name);
            }
        }
    }

    public override void SolvedAction()
    {
        Color color = gameObject.GetComponent<Image>().color;
        color.a = 0;
        gameObject.GetComponent<Image>().color = color;
        gameObject.GetComponent<Image>().raycastTarget = false;
    }
}
