using System;
using UnityEngine;
using UnityEngine.UI;

public class KidRoomSwitch : Trick
{
    [Header("����ġ �̹��� ([0] = On, [1] = Off)")]
    public Sprite[] switchSprite;

    [Header("�Ҳ��� ȿ�� �г�")]
    public GameObject lightPanel;

    [Header("���� �۾�")]
    public GameObject fluorescentTime;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "Switch")
        {
            if (!IsSolved())
            {
                Debug.Log("Switch Off");
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.Log("Switch On");
                SetIsSolved(false);
                SolvedAction();
            }
        }
    }
    public override void SolvedAction()
    {
        this.GetComponent<Image>().sprite = switchSprite[Convert.ToInt32(!IsSolved())];
        lightPanel.SetActive(IsSolved());
        fluorescentTime.SetActive(IsSolved());
    }
}
