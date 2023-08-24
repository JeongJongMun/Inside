using UnityEngine;
using UnityEngine.UI;
using static Define;

public class KillerRoomPostIt2 : Trick
{
    [Header("연필자국 이미지")]
    public Sprite postItPencilMark;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked(ItemName.Pencil2))
            {
                Debug.LogFormat("{0} Solved", this.name);
                Inventory.Instance.RemoveItem(ItemName.Pencil2);
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
