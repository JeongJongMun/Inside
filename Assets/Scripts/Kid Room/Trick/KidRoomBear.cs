using UnityEngine;
using UnityEngine.UI;
using static Define;


public class KidRoomBear : Trick
{
    public Sprite bearBody;
    public GameObject bearHead;
    public GameObject password;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "Bear")
        {
            if (Inventory.Instance.IsClicked(ItemName.Cutter))
            {
                Debug.Log("Bear Solved");
                SetIsSolved(true);
                SolvedAction();
                VoiceManager.Instance.ScreamingMode(RoomName.Kid);
            }
            else
            {
                Debug.Log("Bear Not Solved");
            }
        }

    }
    public override void SolvedAction()
    {
        gameObject.GetComponent<Image>().sprite = bearBody;
        gameObject.GetComponent<Image>().raycastTarget = false;
        bearHead.SetActive(true);
        password.SetActive(true);
    }
}
