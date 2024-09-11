using UnityEngine;
using UnityEngine.UI;

public class LegoHoleBlue : NewTrick
{
#region Private Variables
    private LegoHole legoHole;
#endregion
#region Public Variables
    public Sprite legoInput;
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
        legoHole = transform.parent.GetComponent<LegoHole>();
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (_currentClickedItem.itemName != Define.ItemName.LegoBlue || IsComplete) return false;
        Managers.Sound.Play("lego");
        NewInventory.instance.RemoveItem(_currentClickedItem);
        return true;
    }
    // ReSharper disable Unity.PerformanceAnalysis
    protected override void OnComplete()
    {
        base.OnComplete();
        GetComponent<Image>().sprite = legoInput;
        legoHole.OnLegoComplete(this);
    }
#endregion
}