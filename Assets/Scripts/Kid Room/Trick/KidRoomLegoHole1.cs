using UnityEngine;
using static Define;

public class KidRoomLegoHole1 : Trick
{
    [Header("레고 1")]
    public GameObject lego1;
    [Header("부모 레고 구멍")]
    public GameObject legoHole;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == name)
        {
            if (Inventory.Instance.IsClicked(ItemName.Lego1))
            {
                Debug.LogFormat("{0} is Solved", name);
                
                SoundManager.instance.SFXPlay("lego");
                Inventory.Instance.RemoveItem(ItemName.Lego1);
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
        lego1.SetActive(true);
    }
}
