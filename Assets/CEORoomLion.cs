using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class CEORoomLion : Trick
{
    public Sprite lionZoomOpen;
    public Image lionZoom;
    public GameObject cubeBlue;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked(ItemName.DeadParrot) && !IsSolved())
            {
                Debug.LogFormat("{0} Solved", this.name);
                Inventory.Instance.RemoveItem(ItemName.DeadParrot);
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
        lionZoom.sprite = lionZoomOpen;
        lionZoom.raycastTarget = false;
        cubeBlue.SetActive(true);
    }
}
