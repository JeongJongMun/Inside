using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;


public class KidRoomSafe : Trick
{
    public TMP_Text display;

    [Header("����2 �ݰ� ������ ��������Ʈ")]
    public Sprite safeOpen;

    [Header("����2 �ݰ�")]
    public GameObject safe;

    [Header("����2 Ȯ�� �ݰ� ������")]
    public GameObject safeOpenZoomIn;

    [Header("������")]
    public GameObject latch0;
    public GameObject lego3;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (display.text == "0710")
            {
                Debug.LogFormat("{0} Solved", this.name);

                SoundManager.instance.SFXPlay("electricOKButton");
                Inventory.instance.RemoveItem(ItemName.Password);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                SoundManager.instance.SFXPlay("electricFail");
                display.text = "";
            }
        }

    }
    public override void SolvedAction()
    {
        safe.GetComponent<Image>().sprite = safeOpen;
        safeOpenZoomIn.SetActive(true);
        latch0.SetActive(true);
        lego3.SetActive(true);
    }

    public void OnClickKeypad(GameObject keypad)
    {
        SoundManager.instance.SFXPlay("electricButton");
        if (display.text.Length < 4)
        {
            display.text += keypad.name;
        }
    }
    public void OnClickDelete()
    {
        SoundManager.instance.SFXPlay("electricButton");
        string currentText = display.text;

        if (currentText.Length > 0)
        {
            currentText = currentText.Substring(0, currentText.Length - 1);

            display.text = currentText;
        }
    }
}
