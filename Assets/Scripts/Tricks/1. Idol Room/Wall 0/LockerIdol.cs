using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockerIdol : NewTrick
{
#region Private Variables
    public List<TMP_Text> passwords = new List<TMP_Text>();
    private const string ANSWER = "1004";
#endregion

#region Public Variables
    public Transform passwordHolder;
    public GameObject lockerOpen;
#endregion

#region Public Methods
    public void OnClickPassword(int index)
    {
        GameManager.instance.soundManager.Play("Button");
        int number = (int.Parse(passwords[index].text) + 1) % 10;
        passwords[index].text = number.ToString();
        IsComplete = CheckComplete(null);
    }
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
        lockerOpen.SetActive(false);
        foreach (Transform password in passwordHolder) {
            password.gameObject.GetComponent<Button>().onClick.AddListener(() => OnClickPassword(int.Parse(password.gameObject.name)));
            passwords.Add(password.GetChild(0).GetComponent<TMP_Text>());
        }
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (passwords[0].text + passwords[1].text + passwords[2].text + passwords[3].text != ANSWER || IsComplete) return false;
        GameManager.instance.soundManager.Play("lockerOpen");
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        lockerOpen.SetActive(true);
        gameObject.SetActive(false);
    }
#endregion
}