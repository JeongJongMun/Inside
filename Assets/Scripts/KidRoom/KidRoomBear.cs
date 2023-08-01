using UnityEngine;
using UnityEngine.UI;


public class KidRoomBear : Trick
{
    public Sprite bearBody;
    public GameObject bearHead;
    public GameObject password;
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (obj.name == "Bear")
        {
            if (Inventory.Instance.IsClicked("Cutter"))
            {
                Debug.Log("Bear Solved");
                Solve();
                gameObject.GetComponent<Image>().sprite = bearBody;
                gameObject.GetComponent<Image>().raycastTarget = false;
                bearHead.SetActive(true);
                password.SetActive(true);
            }
            else
            {
                Debug.Log("Bear Not Sloved");
            }
        }

    }
}
