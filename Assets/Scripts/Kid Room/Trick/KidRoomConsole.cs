using UnityEngine;
using UnityEngine.UI;

public class KidRoomConsole : Trick
{
    public bool isGameWin = false;
    public GameObject bookShelf;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == name)
        {
            if (isGameWin)
            {
                Debug.LogFormat("{0} is Solved", name);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} is Not Solved", name);
            }
        }
    }
    public override void SolvedAction()
    {
        bookShelf.transform.position += Vector3.left * 500;
        bookShelf.GetComponent<Image>().raycastTarget = false;
    }
    public void OnClickSkipBtn()
    {
        isGameWin = true;
        TrySolve(gameObject);
    }
}
