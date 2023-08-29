using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KillerRoomLockerKiller : Trick
{
    [Header("정답")]
    public List<string> answers = new List<string>()
    { "Up", "Up", "Left", "Right",};

    [Header("입력 방향")]
    public List<string> inputs = new List<string>();

    [Header("자물쇠 확대 이미지")]
    public Image lockerZoom;

    [Header("트릭 성공 시 풀린 자물쇠 확대 이미지")]
    public Sprite lockerZoomOpen;

    [Header("트릭 성공 시 뚜껑 사라짐")]
    public GameObject lid;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Enumerable.SequenceEqual(inputs, answers))
            {
                Debug.LogFormat("{0} Solved", this.name);
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
        lockerZoom.sprite = lockerZoomOpen;
        lid.SetActive(false);
        gameObject.SetActive(false);
    }

}
