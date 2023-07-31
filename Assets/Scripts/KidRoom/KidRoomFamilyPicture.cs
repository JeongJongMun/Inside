using UnityEngine;
using UnityEngine.UI;

public class KidRoomFamilyPicture : Trick
{
    public Sprite familyPictureTorn;
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (obj.name == "FamilyPicture" && Inventory.Instance.HasItem("Cutter"))
        {
            Debug.Log("FamilyPicture Solved");
            gameObject.GetComponent<Image>().sprite = familyPictureTorn;
            gameObject.GetComponent<Image>().raycastTarget = false;
        }
        else if (obj.name == "FamilyPicture")
        {
            Debug.Log("FamilyPicture Not Solved");
        }
    }
}
