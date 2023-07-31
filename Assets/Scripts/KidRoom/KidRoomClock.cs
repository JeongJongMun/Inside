using System;
using UnityEngine;

public class KidRoomClock : Trick
{
    public GameObject hour;

    [SerializeField]
    private float hourAngle = -90f;
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (hourAngle == -330f && obj.name == "Clock")
        {
            Debug.Log("Clock Solved");
            Solve();
            hourAngle = (hourAngle - 30f) % 360f;
            hour.transform.localEulerAngles = new Vector3(0f, 0f, hourAngle);
        }
        else if (!IsSolved() && obj.name == "Clock")
        {
            Debug.Log("Clock Not Sloved");
            // 시침 한시간 이동
            hourAngle -= 30f;
            hour.transform.localEulerAngles = new Vector3(0f, 0f, hourAngle);
        }
    }
}
