using System;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
#region Private Variables
    private bool isOn = false;
    Image image;
#endregion

#region Public Variables
    public Sprite[] switchSprites;
    public GameObject lightPanel;
    public GameObject fluorescentTime;
#endregion

#region Private Methods
    private void Awake()
    {
        image = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        Managers.Sound.Play("lightswitch");
        isOn = !isOn;
        image.sprite = switchSprites[Convert.ToInt32(!isOn)];
        lightPanel.SetActive(isOn);
        fluorescentTime.SetActive(isOn);
    }
#endregion
}
