using UnityEngine;
using UnityEngine.UI;

public class KidRoomClock : Trick
{
    public GameObject hour;

    [SerializeField]
    private float hourAngle = -90f;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "Clock")
        {
            if (hourAngle == -330f)
            {
                Debug.Log("Clock Solved");
                SetIsSolved(true);
                SolvedAction();
            }
            else if (!IsSolved())
            {
                Debug.Log("Clock Not Solved");

                TicTok();
            }
        }
    }
    public override void SolvedAction()
    {
        hourAngle = -360f;
        hour.transform.localEulerAngles = new Vector3(0f, 0f, hourAngle);

        GetComponent<Image>().raycastTarget = false;

        // 시계 덜렁
        gameObject.transform.position += Vector3.down * 70;
        gameObject.transform.Rotate(0, 0, 30);
    }

    // 시침 한 시간 이동
    void TicTok()
    {
        hourAngle -= 30f;
        hour.transform.localEulerAngles = new Vector3(0f, 0f, hourAngle);
    }
}
