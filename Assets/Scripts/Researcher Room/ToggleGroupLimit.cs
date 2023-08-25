using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupLimit : MonoBehaviour
{
    [Header("자물쇠 숫자 버튼들")]
    public Toggle[] toggles;

    [Header("버튼 최대 클릭 제한")]
    public int maxSelected = 3;

    [SerializeField]
    [Header("현재 입력 숫자들")]
    public List<int> selectedNumbers = new List<int>();

    private void Start()
    {
        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.AddListener((value) => ToggleValueChanged(toggle));
        }
    }

    private void ToggleValueChanged(Toggle toggle)
    {
        int currentSelectedCount = 0;

        foreach (var t in toggles)
        {
            if (t.isOn)
            {
                SoundManager.instance.SFXPlay("lego");
                currentSelectedCount++;
                if (!selectedNumbers.Contains(int.Parse(t.name)))
                {
                    selectedNumbers.Add(int.Parse(t.name));
                }

            }
            else
            {
                selectedNumbers.Remove(int.Parse(t.name));
            }
        }

        if (currentSelectedCount > maxSelected)
        {
            toggle.isOn = false;
        }
    }
}
