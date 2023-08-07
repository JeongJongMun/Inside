using UnityEngine;
using UnityEngine.UI;

public class KidRoomDrawer : Trick
{
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "DrawerZoom")
        {
            if (Inventory.Instance.IsClicked("KidRoomKey"))
            {
                Debug.Log("Drawer Solved");
                Inventory.Instance.RemoveItem("KidRoomKey");
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.Log("Drawer Not Sloved");
            }
        }
    }
    public override void SolvedAction()
    {
        Color color = GetComponent<Image>().color;
        color.a = 0;
        GetComponent<Image>().color = color;
        GetComponent<Image>().raycastTarget = false;
    }
}
