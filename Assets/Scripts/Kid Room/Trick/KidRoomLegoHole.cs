using UnityEngine;

public class KidRoomLegoHole : Trick
{
    [SerializeField]
    public int legoCount = 0;

    [Header("벽면 4의 책")]
    public GameObject book;

    [Header("벽면 4의 책 떨어진거")]
    public GameObject bookDrop;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == name)
        {
            if (legoCount == 3)
            {
                Debug.LogFormat("LegoHole Solved");

                Solved();
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("LegoHole Not Solved");
            }
        }

    }
    public override void SolvedAction()
    {
        book.SetActive(false);
        bookDrop.SetActive(true);
    }
}
