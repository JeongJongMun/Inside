using UnityEngine;
using static Define;

public class KidRoomLegoHole3 : Trick
{
    [Header("���� 3")]
    public GameObject lego3;
    [Header("�θ� ���� ����")]
    public GameObject legoHole;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == name)
        {
            if (Inventory.instance.IsClicked(ItemName.Lego3))
            {
                Debug.LogFormat("{0} is Solved", name);
                
                SoundManager.instance.SFXPlay("lego");
                Inventory.instance.RemoveItem(ItemName.Lego3);
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
        lego3.SetActive(true);
    }
}
