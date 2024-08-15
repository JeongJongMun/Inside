using UnityEngine;
using UnityEngine.UI;
/* WorldMap.cs
 * 0. Kid Room - Wall 2
 */
public class WorldMap : NewTrick
{
#region Public Variables
    public Sprite worldMapTorn;
#endregion

#region Protected Methods
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (_currentClickedItem.itemName != Define.ItemName.Cutter || IsComplete) return false;
        GameManager.instance.soundManager.Play("cutter");
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        Image image = GetComponent<Image>();
        image.sprite = worldMapTorn;
        image.raycastTarget = false;
    }
#endregion
}