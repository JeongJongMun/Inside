using UnityEngine;

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
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Drawer Not Sloved");
            }
        }
    }
}
