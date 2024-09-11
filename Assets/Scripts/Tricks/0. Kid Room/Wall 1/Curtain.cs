using UnityEngine;
using UnityEngine.UI;
/* Curtain.cs
 * 0. Kid Room - Wall 1
 */
public class Curtain : MonoBehaviour
{
#region Private Variables
    private Image curtainImage;
#endregion

#region Public Variables
    public Sprite[] curtainSprites;
#endregion

#region Private Methods
    private void Awake()
    {
        curtainImage = gameObject.GetComponent<Image>();
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        Managers.Sound.Play("curtain");
        curtainImage.sprite = curtainImage.sprite == curtainSprites[0] ? curtainSprites[1] : curtainSprites[0];
    }
#endregion
}