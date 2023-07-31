using UnityEngine;

public class KidRoomDrawer : Trick
{
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (Inventory.Instance.HasItem("KidRoomKey") && obj.name == "Drawer")
        {
            Debug.Log("Drawer Solved");
            Solve();
            Inventory.Instance.RemoveItem("KidRoomKey");
            gameObject.SetActive(false);
        }
        else if (obj.name == "Drawer")
        {
            Debug.Log("Drawer Not Sloved");
        }
    }
}
