using System.Linq;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class KidRoomSafe : Trick
{
    public TMP_Text display;
    public override void SolveOrNotSolve(GameObject obj)
    {
        if (obj.name == "Safe")
        {
            if (display.text == "0710")
            {
                Solve();
                gameObject.SetActive(false);
                Debug.Log("Safe Solved");
            }
            else
            {
                Debug.Log("Safe Not Solved");
            }
        }

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
