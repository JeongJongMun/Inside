using UnityEngine;
using UnityEngine.UI;

public class NewInventorySlot : MonoBehaviour
{
#region Private Variables
    private Image icon;
    private const int iconSize = 150;
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
        // _icon의 원본 이미지 비율을 유지하면서 icon의 크기를 조절
        float ratio = _icon.rect.width / _icon.rect.height;
        icon.rectTransform.sizeDelta = ratio > 1 ? new Vector2(iconSize, iconSize / ratio) : new Vector2(iconSize * ratio, iconSize);
    }
    public void RemoveIcon()
    {
        icon.sprite = null;
        icon.enabled = false;
    }
#endregion
}
