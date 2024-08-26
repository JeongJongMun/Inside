using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CustomList : List<string> 
{
    public LockerBlue lockerBlue;
    public new void Add(string item) 
    { 
        base.Add(item);
        lockerBlue.inputText.text += $"{item} ";
        if (!lockerBlue.CheckCount()) return;
        
        lockerBlue.CheckCompleteWrapper();
        lockerBlue.AnswerClear();
    }
}
public class LockerBlue : NewTrick
{
#region Private Variables
    private List<string> answers = new List<string> { "Left", "Right", "Right", "Down", "Down", "Right", "Left", "Left", "Left", "Down", "Right", "Left"};
#endregion

#region Public Variables
    public CustomList inputs;
    public TMP_Text inputText;
    public Sprite lockerBlueOpenZoom;   // 열린 자물쇠 이미지
    public GameObject[] lockerBlues;       // 잠긴 자물쇠들
    public Image drawer;                // 닫혀있는 서랍
    public Sprite drawerOpen;           // 열린 서랍 이미지

    [Space(10), Header("Items")]
    public GameObject coins;
    public GameObject latch2;
#endregion

#region Private Methods
    private void OnDisable() => AnswerClear();
#endregion

#region Public Methods
    public void CheckCompleteWrapper() => IsComplete = CheckComplete(null);
    public bool CheckCount() => inputs.Count == answers.Count;
    public void AnswerClear()
    {
        inputs.Clear();
        inputText.text = "";
    }
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
        inputs = new CustomList {
            lockerBlue = this
        };
        coins.SetActive(false);
        latch2.SetActive(false);
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (!Enumerable.SequenceEqual(inputs, answers) || IsComplete) return false;

        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();

        GetComponent<Image>().sprite = lockerBlueOpenZoom;
        foreach (GameObject lockerBlue in lockerBlues) {
            lockerBlue.SetActive(false);
        }
        drawer.sprite = drawerOpen;

        // Items
        coins.SetActive(true);
        latch2.SetActive(true);

        // Direction Button Disable
        DirectionButton directionButtons = GetComponentInChildren<DirectionButton>();
        directionButtons.gameObject.GetComponent<Image>().raycastTarget = false;
    }
#endregion
}