using UnityEngine;
using UnityEngine.UI;

public class KidRoomLegoHole : Trick
{
    [SerializeField]
    private int legoCount = 0;

    public Sprite[] legoInput; // 레고 1, 2, 3 들어간 스프라이트
    public GameObject book, bookZoom;
    public GameObject bookDrop;

    public override void SolveOrNotSolve(GameObject obj)
    {
        if (obj.name == gameObject.name)
        {
            if (Inventory.Instance.IsClicked("Lego" + obj.name[4]))
            {
                Debug.LogFormat("{0} Solved", obj.name);

                int legoNum = obj.name[4] - '0';
                gameObject.GetComponent<Image>().sprite = legoInput[legoNum - 1];

                Inventory.Instance.RemoveItem(obj.name.Substring(0,5));

                legoCount++;
                if (legoCount == 3)
                {
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
