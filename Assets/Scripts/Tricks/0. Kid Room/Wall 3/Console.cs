using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Console : NewTrick
{
#region Public Variables
    public bool isGameWin = false;
    public GameObject bookShelf;
#endregion

#region Protected Methods
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (!isGameWin) return false;

        GameManager.instance.soundManager.Play("gameClear");
        StartCoroutine(DoorSlideSoundEffect());
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        bookShelf.transform.position += Vector3.left * 500;
        bookShelf.GetComponent<Image>().raycastTarget = false;
        this.GetComponent<Image>().raycastTarget = false;
    }
#endregion

    private IEnumerator DoorSlideSoundEffect()
    {
        yield return new WaitForSeconds(2.0f);
        GameManager.instance.soundManager.Play("doorSlide");
    }
    public void Clear()
    {
        isGameWin = true;
        IsComplete = CheckComplete(null);
    }
}
