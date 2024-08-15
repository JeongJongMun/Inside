using UnityEngine;
using UnityEngine.UI;

public class Bed : NewTrick
{
#region Public Variables
    public Sprite bedOpen;
#endregion

#region Protected Methods
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        GameManager.instance.soundManager.Play("bedFabric");
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        GetComponent<Image>().sprite = bedOpen;
    }
#endregion
}