using UnityEngine;

public class KidRoomLegoHole3 : Trick
{
    [Header("레고 3")]
    public GameObject lego3;
    [Header("부모 레고 구멍")]
    public GameObject legoHole;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == name)
        {
            if (Inventory.Instance.IsClicked("Lego3"))
            {
                Debug.LogFormat("{0} is Solved", name);
                Inventory.Instance.RemoveItem("Lego3");
                SetIsSolved(true);
                SolvedAction();
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
        legoHole.GetComponent<KidRoomLegoHole>().TrySolve(legoHole);
        lego3.SetActive(true);
    }
}
