using UnityEngine;
using UnityEngine.UI;

public class NewInventorySlot : MonoBehaviour
{
#region Private Variables
    private Image icon;
#endregion

#region Public Variables

#endregion

#region Private Methods
    private void Awake()
    {
        icon = this.transform.GetChild(2).GetComponent<Image>();
        icon.enabled = false;
    }
#endregion

#region Public Methods
    public void SetIcon(Sprite _icon)
    {
        icon.sprite = _icon;
        icon.enabled = true;
    }
    public void RemoveIcon()
    {
        icon = null;
        icon.enabled = false;
    }
#endregion
}
