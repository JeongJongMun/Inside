using UnityEngine;
using UnityEngine.UI;

public class CabinetResearcher : NewTrick
{
#region Private Variables
#endregion

#region Public Variables
    public Sprite cabinetOpened;
    public GameObject[] testTubes;
#endregion

#region Protected Methods
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (_currentClickedItem.itemName != Define.ItemName.GoldKey) {
            GameManager.instance.soundManager.Play("drawerLocked");
            return false;
        }
        NewInventory.instance.RemoveItem(_currentClickedItem);
        GameManager.instance.soundManager.Play("closet");
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        GetComponent<Image>().sprite = cabinetOpened;
        foreach (GameObject testTube in testTubes) {
            testTube.SetActive(true);
        }
    }
#endregion
}