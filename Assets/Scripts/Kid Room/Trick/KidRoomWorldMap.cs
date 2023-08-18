using UnityEngine;
using UnityEngine.UI;
using static Define;

public class KidRoomWorldMap : Trick
{
    public AudioClip mapClip;

    public Sprite worldMapTorn;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "WorldMap")
        {
            if (Inventory.Instance.IsClicked(ItemName.Cutter))
            {
                SoundManager.instance.SFXPlay("Map", mapClip);
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
