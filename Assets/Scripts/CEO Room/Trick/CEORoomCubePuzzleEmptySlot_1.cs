using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CEORoomCubePuzzleEmptySlot_1 : Trick
{
    [Header("�� ���Կ� ä��� ����")]
    [SerializeField]
    private List<EItemType> slots = new List<EItemType>()
    { EItemType.CubeBlue, EItemType.CubeRed, EItemType.CubeYellow };

    [Header("Cube slot 0~2")]
    public GameObject[] cubeSlots;

    [SerializeField]
    private int cubeNumber;

    public override void Start()
    {
        base.Start();
        cubeNumber = int.Parse(name.Substring(9));
    }

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.instance.IsClicked(slots[cubeNumber]))
            {
                Debug.LogFormat("{0} Solved", this.name);
                Inventory.instance.RemoveItem(slots[cubeNumber]);
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
        cubeSlots[cubeNumber].SetActive(true);
        gameObject.SetActive(false);
    }
}
