using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum Notes {
    C, D, E, F, G
}

public class MusicPlate : NewTrick
{
#region Private Variables
    private Dictionary<Notes, int> notePosition = new Dictionary<Notes, int>
    {
        { Notes.C, -65 }, // 1
        { Notes.D, -45 }, // 2
        { Notes.E, -25 }, // 3
        { Notes.F, -5 }, // 4
        { Notes.G, 15 }, // 5
    };
    private int noteCount = 0;
    private List<Notes> input = new List<Notes>();
    // 정답: 533422
    private List<Notes> answer = new List<Notes>() {
        Notes.G, Notes.E, Notes.E, Notes.F, Notes.D, Notes.D
    };
    private List<GameObject> notes = new List<GameObject>();
    private List<GameObject> noteButtons = new List<GameObject>();
#endregion

#region Public Variables
    public Transform noteHolder;
    public Transform noteButtonHolder;
#endregion

#region Private Methods
    private void GetNotes()
    {
        foreach (Transform note in noteHolder) {
            notes.Add(note.gameObject);
            note.gameObject.SetActive(false);
        }

        foreach (Transform noteButton in noteButtonHolder) {
            GameObject noteButtonObject = noteButton.gameObject;
            noteButtons.Add(noteButtonObject);
            noteButtonObject.GetComponent<Button>().onClick.AddListener(() => OnClickNoteButton(noteButtonObject.name));
        }
    }
    private void OnClickNoteButton(string name)
    {
        if (noteCount >= 6) return;

        Notes note = (Notes)Enum.Parse(typeof(Notes), name);

        notes[noteCount].SetActive(true);

        Vector2 pos = notes[noteCount].GetComponent<RectTransform>().anchoredPosition;
        pos.y = notePosition[note];
        notes[noteCount].GetComponent<RectTransform>().anchoredPosition = pos;

        input.Add(note);

        noteCount++;
        if (noteCount == notes.Count) {
            IsComplete = CheckComplete(null);
        }
    }
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
        GetNotes();
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (!Enumerable.SequenceEqual(input, answer)) {
            foreach (GameObject note in notes) {
                note.SetActive(false);
            }
            input.Clear();
            noteCount = 0;
            GameManager.instance.soundManager.Play("electricFail");
            return false;
        }
        GameManager.instance.soundManager.Play("doorOpen");

        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        for (int i = 0; i < notes.Count; i++) {
            notes[i].SetActive(true);

            Vector2 pos = notes[i].GetComponent<RectTransform>().anchoredPosition;
            pos.y = notePosition[answer[i]];
            notes[i].GetComponent<RectTransform>().anchoredPosition = pos;
        }

        foreach (GameObject noteButton in noteButtons) {
            noteButton.GetComponent<Image>().raycastTarget = false;
        }
    }
#endregion
}
