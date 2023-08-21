using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;  

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

                SoundManager.instance.SFXPlay("gameClear");
                StartCoroutine(ForDelay());
                
            }
            else
            {
                Debug.LogFormat("{0} is Not Solved", name);
            }
        }
    }

    private IEnumerator ForDelay()
    {
        SetIsSolved(true);
        yield return new WaitForSeconds(2.0f);
        SoundManager.instance.SFXPlay("doorSlide");
        SolvedAction();
    }

    public override void SolvedAction()
    {
        bookShelf.transform.position += Vector3.left * 500;
        bookShelf.GetComponent<Image>().raycastTarget = false;
        this.GetComponent<Image>().raycastTarget = false;
    }
    public void OnClickSkipBtn()
    {
        SoundManager.instance.SFXPlay("buttonSound");
        isGameWin = true;
        TrySolve(gameObject);
    }
}
