using UnityEngine;

public class KillerRoomHangerHole : Trick
{
    [Header("Hanger ������")]
    public GameObject hangerInput;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.instance.IsClicked(Define.EItemType.Hanger))
            {
                Debug.LogFormat("{0} Solved", this.name);
                Inventory.instance.RemoveItem(Define.EItemType.Hanger);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
            }
        }
    }


    public override void SolvedAction()
    {
        hangerInput.SetActive(true);
    }
}
