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
    [Header("6개 음표")]
    public GameObject[] notes;

    [Header("음별 Y 위치")]
    Dictionary<Notes, int> notesPosition = new Dictionary<Notes, int>
    {
        { Notes.C, -65 }, // 1
        { Notes.D, -45 }, // 2
        { Notes.E, -25 }, // 3
        { Notes.F, -5 }, // 4
        { Notes.G, 15 }, // 5
    };

    [SerializeField]
    [Header("현재 음표 번호")]
    private int currentNoteNumber = 0;

    [SerializeField]
    [Header("현재 입력 값")]
    private List<Notes> input = new List<Notes>();

    [Header("정답: 533422")]
    private List<Notes> answer = new List<Notes>() 
    {
        Notes.G, Notes.E, Notes.E, Notes.F, Notes.D, Notes.D
    };


    [Header("음표 버튼들")]
    public Image[] noteButtons;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            // 입력과 정답이 같을때
            if (Enumerable.SequenceEqual(input, answer))
            {
                Debug.LogFormat("{0} Solved", this.name);
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
            // 음표 활성화
            notes[i].SetActive(true);

            // 음 별 y값 설정
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
            // 음표 오브젝트의 이름을 Enum으로 변환
            Notes _note = (Notes)Enum.Parse(typeof(Notes), noteButton.name);

            // 음표 활성화
            notes[currentNoteNumber].SetActive(true);

            // 음 별 y값 설정
            Vector3 _transform = notes[currentNoteNumber].GetComponent<RectTransform>().anchoredPosition;
            _transform.y = notesPosition[_note];
            notes[currentNoteNumber].GetComponent<RectTransform>().anchoredPosition = _transform;

            // 입력 기록
            input.Add(_note);

            // 다음 음표로 이동
            currentNoteNumber++;

            // 음표를 모두 입력했을 시 0.2초 보여주고 TrySolve
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
