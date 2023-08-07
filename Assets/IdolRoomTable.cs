using UnityEngine;

public class IdolRoomTable : Trick
{
    [Header("¿­¸° ¿À¸£°ñ")]
    public GameObject musicBoxOpened;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked("MusicBox"))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SetIsSolved(true);
                SolvedAction();
                Inventory.Instance.RemoveItem("MusicBox");
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
            }
        }
    }
    public override void SolvedAction()
    {
        musicBoxOpened.SetActive(true);
        gameObject.SetActive(false);
    }
}
