using UnityEngine;

public class CEORoomParrot : Trick
{
    public GameObject deadParrot;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.instance.IsClicked(Define.EItemType.Gun))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("parrotDead");
                SoundManager.instance.SFXPlay("gunshot");
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                SoundManager.instance.SFXPlay("parrot");
            }
        }
    }
    public override void SolvedAction()
    {
        deadParrot.SetActive(true);
        gameObject.SetActive(false);
    }
}
