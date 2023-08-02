using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidRoomConsole : Trick
{
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (obj.name == "Console")
        {
            Debug.Log("Console Clicked");
        }
    }
}
