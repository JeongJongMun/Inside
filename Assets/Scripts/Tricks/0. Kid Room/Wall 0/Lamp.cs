using UnityEngine;
using UnityEngine.UI;
/* Lamp.cs
 * 0. Kid Room - Wall 0
 * NOT Inherited from Observer
 */
public class Lamp : MonoBehaviour
{
#region Private Variables
    private Image lampImage;
#endregion

#region Public Variables
    public Sprite[] lampSprites;
#endregion

#region Private Methods
    private void Awake()
    {
        lampImage = gameObject.GetComponent<Image>();
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        // SoundManager.instance.SFXPlay("lampswitch");
        lampImage.sprite = lampImage.sprite == lampSprites[0] ? lampSprites[1] : lampSprites[0];
    }
#endregion

#region Public Methods
#endregion
}