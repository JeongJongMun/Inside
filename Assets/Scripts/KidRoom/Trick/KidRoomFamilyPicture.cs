using UnityEngine;
using UnityEngine.UI;

public class KidRoomFamilyPicture : Trick
{
    public Sprite familyPictureTorn;
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (obj.name == "FamilyPicture")
        {
            if (Inventory.Instance.IsClicked("Cutter"))
            {
                Debug.Log("FamilyPicture Solved");
                gameObject.GetComponent<Image>().sprite = familyPictureTorn;
                gameObject.GetComponent<Image>().raycastTarget = false;
            }
            else
            {
                Debug.Log("FamilyPicture Not Solved");
            }
        }

    }
}
