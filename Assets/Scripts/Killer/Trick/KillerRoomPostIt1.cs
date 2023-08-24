using UnityEngine;
using UnityEngine.UI;
using static Define;

public class KillerRoomPostIt1 : Trick
{
    [Header("연필자국 이미지")]
    public Sprite postItPencilMark;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked(ItemName.Pencil1))
            {
                Debug.LogFormat("{0} Solved", this.name);
                Inventory.Instance.RemoveItem(ItemName.Pencil1);
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
        GetComponent<Image>().sprite = postItPencilMark;
    }
}
