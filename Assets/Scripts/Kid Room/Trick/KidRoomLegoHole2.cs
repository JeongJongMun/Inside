using UnityEngine;
using static Define;

public class KidRoomLegoHole2 : Trick
{
    [Header("레고 2")]
    public GameObject lego2;
    [Header("부모 레고 구멍")]
    public GameObject legoHole;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == name)
        {
            if (Inventory.Instance.IsClicked(ItemName.Lego2))
            {
                Debug.LogFormat("{0} is Solved", name);

                SoundManager.instance.SFXPlay("lego");
                Inventory.Instance.RemoveItem(ItemName.Lego2);
                SetIsSolved(true);
                SolvedAction();
                legoHole.GetComponent<KidRoomLegoHole>().TrySolve(legoHole);
            }
            else
            {
                Debug.LogFormat("{0} is Not Solved", name);
            }
        }
    }
    public override void SolvedAction()
    {
        legoHole.GetComponent<KidRoomLegoHole>().legoCount++;
        lego2.SetActive(true);
    }
}
