using UnityEngine;
using UnityEngine.UI;

public class KidRoomDrawer : Trick
{
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (obj.name == "Drawer")
        {
            if (Inventory.Instance.IsClicked("KidRoomKey"))
            {
                Debug.Log("Drawer Solved");
                Solve();
                Inventory.Instance.RemoveItem("KidRoomKey");

                Color color = GetComponent<Image>().color;
                color.a = 0;
                GetComponent<Image>().color = color;
                GetComponent<Image>().raycastTarget = false;
            }
            else
            {
                Debug.Log("Drawer Not Sloved");
            }
        }
    }
}
