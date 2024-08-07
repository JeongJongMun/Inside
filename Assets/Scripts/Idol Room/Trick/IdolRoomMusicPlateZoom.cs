using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum Notes
{
    C,
    D,
    E,
    F,
    G
}

public class IdolRoomMusicPlateZoom : Trick
{
    [Header("6�� ��ǥ")]
    public GameObject[] notes;

    [Header("���� Y ��ġ")]
    Dictionary<Notes, int> notesPosition = new Dictionary<Notes, int>
    {
        { Notes.C, -65 }, // 1
        { Notes.D, -45 }, // 2
        { Notes.E, -25 }, // 3
        { Notes.F, -5 }, // 4
        { Notes.G, 15 }, // 5
    };

    [SerializeField]
    [Header("���� ��ǥ ��ȣ")]
    private int currentNoteNumber = 0;

    [SerializeField]
    [Header("���� �Է� ��")]
    private List<Notes> input = new List<Notes>();

    [Header("����: 533422")]
    private List<Notes> answer = new List<Notes>() 
    {
        Notes.G, Notes.E, Notes.E, Notes.F, Notes.D, Notes.D
    };


    [Header("��ǥ ��ư��")]
    public Image[] noteButtons;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            // �Է°� ������ ������
            if (Enumerable.SequenceEqual(input, answer))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("electricDoorOpen");
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                foreach (GameObject note in notes)
                {
                    note.SetActive(false);
                }
                Debug.LogFormat("{0} Not Solved", this.name);
                input.Clear();
                currentNoteNumber = 0;
            }
        }
    }
    public override void SolvedAction()
    {
        for (int i = 0; i < notes.Length; i++)
        {
            // ��ǥ Ȱ��ȭ
            notes[i].SetActive(true);

            // �� �� y�� ����
            Vector3 _transform = notes[i].GetComponent<RectTransform>().anchoredPosition;
            _transform.y = notesPosition[answer[i]];
            notes[i].GetComponent<RectTransform>().anchoredPosition = _transform;
        }

        foreach (Image button in noteButtons)
        {
            button.raycastTarget = false;
        }
    }

    public void OnClickNoteButton(GameObject noteButton)
    {
        if (currentNoteNumber < 6)
        {
            // ��ǥ ������Ʈ�� �̸��� Enum���� ��ȯ
            Notes _note = (Notes)Enum.Parse(typeof(Notes), noteButton.name);

            // ��ǥ Ȱ��ȭ
            notes[currentNoteNumber].SetActive(true);

            // �� �� y�� ����
            Vector3 _transform = notes[currentNoteNumber].GetComponent<RectTransform>().anchoredPosition;
            _transform.y = notesPosition[_note];
            notes[currentNoteNumber].GetComponent<RectTransform>().anchoredPosition = _transform;

            // �Է� ���
            input.Add(_note);

            // ���� ��ǥ�� �̵�
            currentNoteNumber++;
            // SoundManager.instance.pianoPlay("piano", (int)_note);

            // ��ǥ�� ��� �Է����� �� 0.2�� �����ְ� TrySolve
            if (currentNoteNumber == notes.Length)
            {
                StartCoroutine(DoTrySolve());
            }
        }
    }
    public IEnumerator DoTrySolve()
    {
        yield return new WaitForSeconds(0.2f);
        TrySolve(gameObject);
    }
}
