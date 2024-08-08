using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum Notes
{
    C, D, E, F, G
}

public class MusicPlate : NewTrick
{
#region Private Variables
    private Dictionary<Notes, int> notesPosition = new Dictionary<Notes, int>
    {
        { Notes.C, -65 }, // 1
        { Notes.D, -45 }, // 2
        { Notes.E, -25 }, // 3
        { Notes.F, -5 }, // 4
        { Notes.G, 15 }, // 5
    };
    private int currentNoteNumber = 0;
    private List<Notes> input = new List<Notes>();
    // 정답: 533422
    private List<Notes> answer = new List<Notes>() 
    {
        Notes.G, Notes.E, Notes.E, Notes.F, Notes.D, Notes.D
    };
#endregion

#region Public Variables
    public GameObject[] notes;
    public Image[] noteButtons;
#endregion

#region Private Methods
#endregion

#region Public Methods
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (!Enumerable.SequenceEqual(input, answer)) {
            foreach (GameObject note in notes) {
                note.SetActive(false);
            }
            input.Clear();
            currentNoteNumber = 0;
            return false;
        }
        GameManager.Instance.soundManager.Play("electricDoorOpen");

        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetActive(true);

            Vector3 _transform = notes[i].GetComponent<RectTransform>().anchoredPosition;
            _transform.y = notesPosition[answer[i]];
            notes[i].GetComponent<RectTransform>().anchoredPosition = _transform;
        }

        foreach (Image button in noteButtons)
        {
            button.raycastTarget = false;
        }
    }
#endregion
    public void OnClickNoteButton(GameObject noteButton)
    {
        if (currentNoteNumber < 6)
        {
            Notes _note = (Notes)Enum.Parse(typeof(Notes), noteButton.name);

            notes[currentNoteNumber].SetActive(true);

            Vector3 _transform = notes[currentNoteNumber].GetComponent<RectTransform>().anchoredPosition;
            _transform.y = notesPosition[_note];
            notes[currentNoteNumber].GetComponent<RectTransform>().anchoredPosition = _transform;

            input.Add(_note);

            currentNoteNumber++;
            if (currentNoteNumber == notes.Length) {
                StartCoroutine(DoTrySolve());
            }
        }
    }
    public IEnumerator DoTrySolve()
    {
        yield return new WaitForSeconds(0.2f);
        // TrySolve(gameObject);
    }
}
