using UnityEngine;

public class CEORoomParrot : Trick
{
    public GameObject deadParrot;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked(Define.ItemName.Gun))
            {
                Debug.LogFormat("{0} Solved", this.name);
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
        deadParrot.SetActive(true);
        gameObject.SetActive(false);
    }
}
