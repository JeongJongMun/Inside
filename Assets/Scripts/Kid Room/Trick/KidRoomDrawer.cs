using UnityEngine;
using UnityEngine.UI;
using static Define;

public class KidRoomDrawer : Trick
{
    public AudioClip drawerClosedClip;
    public AudioClip drawerOpenedClip;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "DrawerZoom")
        {
            if (Inventory.Instance.IsClicked(ItemName.KidRoomKey))
            {
                SoundManager.instance.SFXPlay("DrawerOpened", drawerOpenedClip);
                Debug.LogFormat("{0} Solved", this.name);
                Inventory.Instance.RemoveItem(ItemName.KidRoomKey);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                SoundManager.instance.SFXPlay("DrawerClosed", drawerClosedClip);
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
