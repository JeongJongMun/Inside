using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KillerRoomLockerKiller : Trick
{
    [Header("����")]
    public List<string> answers = new List<string>()
    { "Up", "Up", "Left", "Right",};

    [Header("�Է� ����")]
    public List<string> inputs = new List<string>();

    [Header("�ڹ��� Ȯ�� �̹���")]
    public Image lockerZoom;

    [Header("Ʈ�� ���� �� Ǯ�� �ڹ��� Ȯ�� �̹���")]
    public Sprite lockerZoomOpen;

    [Header("Ʈ�� ���� �� �Ѳ� �����")]
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
