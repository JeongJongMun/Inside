using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidRoomWorldMap : Trick
{
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (Inventory.Instance.HasItem("Cutter"))
        {
            Debug.Log("WorldMap Solved");
        }
        else
        {
            Debug.Log("WorldMap Not Sloved");
        }
    }
}
