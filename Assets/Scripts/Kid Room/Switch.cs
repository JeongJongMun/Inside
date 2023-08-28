using System;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    [Header("����ġ �̹��� ([0] = Off, [1] = On)")]
    public Sprite[] switchSprite;

    [Header("�Ҳ��� ȿ�� �г�")]
    public GameObject lightPanel;

    [Header("���� �۾�")]
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
