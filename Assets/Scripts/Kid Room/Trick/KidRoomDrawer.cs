using UnityEngine;
using UnityEngine.UI;
using static Define;

public class KidRoomDrawer : Trick
{
    
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "DrawerZoom")
        {
            if (Inventory.Instance.IsClicked(ItemName.KidRoomKey))
            {
                Debug.LogFormat("{0} Solved", this.name);
                Inventory.Instance.RemoveItem(ItemName.KidRoomKey);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Sloved", this.name);
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
