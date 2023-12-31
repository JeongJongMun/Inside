using UnityEngine;

public class KillerRoomHangerHole : Trick
{
    [Header("Hanger ������")]
    public GameObject hangerInput;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked(Define.ItemName.Hanger))
            {
                Debug.LogFormat("{0} Solved", this.name);
                Inventory.Instance.RemoveItem(Define.ItemName.Hanger);
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
