using UnityEngine;
using UnityEngine.UI;

public class KidRoomFamilyPicture : Trick
{
    public Sprite familyPictureTorn;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "FamilyPicture")
        {
            if (Inventory.Instance.IsClicked("Cutter"))
            {
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
