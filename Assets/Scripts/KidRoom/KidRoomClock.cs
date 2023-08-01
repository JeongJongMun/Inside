using PlayFab.MultiplayerModels;
using System;
using UnityEngine;

public class KidRoomClock : Trick
{
    public GameObject hour;

    [SerializeField]
    private float hourAngle = -90f;
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (obj.name == "Clock")
        {
            if (hourAngle == -330f)
            {
                Debug.Log("Clock Solved");
                Solve();

                // 시침 한시간 이동
                TicTok();

                // 시계 덜렁
                gameObject.transform.position += Vector3.down * 70;
                gameObject.transform.Rotate(0, 0, 30);
            }
            else if (!IsSolved())
            {
                Debug.Log("Clock Not Sloved");

                // 시침 한시간 이동
                TicTok();
            }
        }
    }

    void TicTok()
    {
        hourAngle -= 30f;
        hour.transform.localEulerAngles = new Vector3(0f, 0f, hourAngle);
    }
}
