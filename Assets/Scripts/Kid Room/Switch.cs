using System;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    [Header("½ºÀ§Ä¡ ÀÌ¹ÌÁö ([0] = Off, [1] = On)")]
    public Sprite[] switchSprite;

    [Header("ºÒ²¨Áü È¿°ú ÆÐ³Î")]
    public GameObject lightPanel;

    [Header("Çü±¤ ±Û¾¾")]
    public GameObject fluorescentTime;

    private bool isOn = false;

    public void OnClickSwitch()
    {
        SoundManager.instance.SFXPlay("lightswitch");
        isOn = !isOn;
        GetComponent<Image>().sprite = switchSprite[Convert.ToInt32(!isOn)];
        lightPanel.SetActive(isOn);
        fluorescentTime.SetActive(isOn);
    }
}
