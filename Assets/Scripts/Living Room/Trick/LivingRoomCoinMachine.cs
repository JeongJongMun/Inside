using UnityEngine;

public class LivingRoomCoinMachine : Trick
{
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (money.text == "2200")
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("electricDoorOpen");
                Inventory.Instance.RemoveItem(ItemName.Coins);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                money.text = "0";
            }
        }
    }
    public override void SolvedAction()
    {
        throw new System.NotImplementedException();
    }
}
