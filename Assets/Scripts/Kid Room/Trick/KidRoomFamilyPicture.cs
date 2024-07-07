using UnityEngine;
using UnityEngine.UI;
using static Define;

public class KidRoomFamilyPicture : Trick
{
    public Sprite familyPictureTorn;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "FamilyPicture")
        {
            if (Inventory.instance.IsClicked(ItemName.Cutter))
            {
                SoundManager.instance.SFXPlay("cutter");
                Debug.Log("FamilyPicture Solved");
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.Log("FamilyPicture Not Solved");
            }
        }

    }
    public override void SolvedAction()
    {
        gameObject.GetComponent<Image>().sprite = familyPictureTorn;
        gameObject.GetComponent<Image>().raycastTarget = false;
    }
}
