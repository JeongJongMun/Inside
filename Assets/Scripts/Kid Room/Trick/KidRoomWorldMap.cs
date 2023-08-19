using UnityEngine;
using UnityEngine.UI;
using static Define;

public class KidRoomWorldMap : Trick
{

    public Sprite worldMapTorn;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "WorldMap")
        {
            if (Inventory.Instance.IsClicked(ItemName.Cutter))
            {
                Debug.Log("WorldMap Solved");
                SetIsSolved(true);
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
