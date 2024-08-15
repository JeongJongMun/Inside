using UnityEngine;
using UnityEngine.UI;
/* Lamp.cs
 * 0. Kid Room - Wall 0
 */
public class Lamp : MonoBehaviour
{
#region Private Variables
    private Image image;
    private Lamp[] lamps;
    private static int status = 0;
#endregion

#region Public Variables
    public Sprite[] lampSprites;
#endregion

#region Private Methods
    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        lamps = FindObjectsOfType<Lamp>();
    }
    private void OnEnable() => image.sprite = lampSprites[status];
    private void OnClick()
    {
        GameManager.instance.soundManager.Play("lampswitch");
        status = status == 0 ? 1 : 0;
        foreach (Lamp lamp in lamps) {
            lamp.OnEnable();
        }
    }
#endregion

#region Public Methods
#endregion
}