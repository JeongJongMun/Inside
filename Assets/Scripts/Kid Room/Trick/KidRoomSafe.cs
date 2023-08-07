using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject latch1;
    public GameObject lego3;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "Safe")
        {
            if (display.text == "0710")
            {
                Debug.Log("Safe Solved");
                Inventory.Instance.RemoveItem("Password");
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.Log("Safe Not Solved");
            }
        }

    }
    public override void SolvedAction()
    {
        safe.GetComponent<Image>().sprite = safeOpen;
        safeOpenZoomIn.SetActive(true);
        latch1.SetActive(true);
        lego3.SetActive(true);
    }

    public void OnClickKeypad(GameObject keypad)
    {
        if (display.text.Length < 4)
        {
            display.text += keypad.name;
        }
    }
    public void OnClickDelete()
    {
        string currentText = display.text;

        if (currentText.Length > 0)
        {
            currentText = currentText.Substring(0, currentText.Length - 1);

            display.text = currentText;
        }
    }
}
