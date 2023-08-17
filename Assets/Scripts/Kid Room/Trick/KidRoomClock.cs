using UnityEngine;
using UnityEngine.UI;

public class KidRoomClock : Trick
{
    public GameObject hour;

    public AudioClip clockClip;

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

        // �ð� ����
        gameObject.transform.position += Vector3.down * 70;
        gameObject.transform.Rotate(0, 0, 30);
    }

    // ��ħ �� �ð� �̵�
    void TicTok()
    {
        SoundManager.instance.SFXPlay("Clock", clockClip); // 효과음
        hourAngle -= 30f;
        hour.transform.localEulerAngles = new Vector3(0f, 0f, hourAngle);
    }
}
