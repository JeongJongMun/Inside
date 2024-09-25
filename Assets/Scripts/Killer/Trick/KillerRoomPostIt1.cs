using UnityEngine;
using UnityEngine.UI;
using static Define;

public class KillerRoomPostIt1 : Trick
{
    [Header("�����ڱ� �̹���")]
    public Sprite postItPencilMark;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.instance.IsClicked(EItemType.Pencil1))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("pencil");
                Inventory.instance.RemoveItem(EItemType.Pencil1);
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
