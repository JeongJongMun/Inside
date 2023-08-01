using UnityEngine;
using UnityEngine.UI;

public class KidRoomWorldMap : Trick
{
    public Sprite worldMapTorn;
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (obj.name == "WorldMap")
        {
            if (Inventory.Instance.IsClicked("Cutter"))
            {
                Debug.Log("WorldMap Solved");
                gameObject.GetComponent<Image>().sprite = worldMapTorn;
            }
            else
            {
                Debug.Log("WorldMap Not Sloved");
            }

        }

    }
}
