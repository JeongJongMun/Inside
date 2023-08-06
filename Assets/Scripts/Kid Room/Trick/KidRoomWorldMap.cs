using UnityEngine;
using UnityEngine.UI;

public class KidRoomWorldMap : Trick
{
    public Sprite worldMapTorn;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "WorldMap")
        {
            if (Inventory.Instance.IsClicked("Cutter"))
            {
                Debug.Log("WorldMap Solved");
                Solved();
                SolvedAction();
            }
            else
            {
                Debug.Log("WorldMap Not Sloved");
            }

        }

    }
    public override void SolvedAction()
    {
        gameObject.GetComponent<Image>().sprite = worldMapTorn;
    }
}
