using UnityEngine;
using static Define;

public class KidRoomLegoHole2 : Trick
{
    [Header("���� 2")]
    public GameObject lego2;
    [Header("�θ� ���� ����")]
    public GameObject legoHole;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == name)
        {
            if (Inventory.instance.IsClicked(ItemName.Lego2))
            {
                Debug.LogFormat("{0} is Solved", name);

                SoundManager.instance.SFXPlay("lego");
                Inventory.instance.RemoveItem(ItemName.Lego2);
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
