using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupLimit : MonoBehaviour
{
    [Header("�ڹ��� ���� ��ư��")]
    public Toggle[] toggles;

    [Header("��ư �ִ� Ŭ�� ����")]
    public int maxSelected = 3;

    [SerializeField]
    [Header("���� �Է� ���ڵ�")]
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

        SoundManager.instance.SFXPlay("lego");
        foreach (var t in toggles)
        {
            if (t.isOn)
            {
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
