using UnityEngine;
using UnityEngine.UI;

public class LegoHoleGreen : NewTrick
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
        if (_currentClickedItem.itemName != Define.ItemName.LegoGreen || IsComplete) return false;
        Managers.Sound.Play("lego");
        NewInventory.instance.RemoveItem(_currentClickedItem);
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        GetComponent<Image>().sprite = legoInput;
        legoHole.OnLegoComplete(this);
    }
#endregion
}