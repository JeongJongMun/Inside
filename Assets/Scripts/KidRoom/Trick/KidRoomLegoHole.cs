using UnityEngine;
using UnityEngine.UI;

public class KidRoomLegoHole : Trick
{
    [SerializeField]
    private static int legoCount = 0;


    [Header("����4 ������ ����")]
    public GameObject[] legos;

    [Header("���� 1, 2, 3 �� ��������Ʈ")]
    public Sprite[] legoInput;
    public GameObject book, bookZoom;
    public GameObject bookDrop;

    public override void SolveOrNotSolve(GameObject obj)
    {
        if (obj.name == gameObject.name)
        {
            if (Inventory.Instance.IsClicked("Lego" + obj.name[4]))
            {
                Debug.LogFormat("{0} Solved", obj.name);

                int legoNum = obj.name[4] - '0' - 1;
                Debug.Log(legoNum);
                gameObject.GetComponent<Image>().sprite = legoInput[legoNum];
                legos[legoNum].SetActive(true);

                Inventory.Instance.RemoveItem(obj.name.Substring(0,5));

                legoCount++;
                if (legoCount == 3)
                {
                    Solve();

                    book.SetActive(false);
                    bookZoom.SetActive(false);
                    bookDrop.SetActive(true);
                }
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", obj.name);
            }
        }

    }
}
