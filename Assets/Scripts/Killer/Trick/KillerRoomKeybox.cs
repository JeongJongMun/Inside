using TMPro;
using UnityEngine;

public class KillerRoomKeybox : Trick
{
    [Header("InputField들")]
    public TMP_InputField[] inputfields;

    [Header("비밀번호 정답")]
    private string answer = "want";

    [Header("Keybox Open")]
    public GameObject keyboxOpen;

    public override void Start()
    {
        base.Start();
        foreach (TMP_InputField inputfield in inputfields)
        {
            inputfield.characterLimit = 1;
        }
    }

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (inputfields[0].text + inputfields[1].text + inputfields[2].text + inputfields[3].text == answer)
            {
                Debug.LogFormat("{0} Solved", this.name);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
            }
        }
    }


    public override void SolvedAction()
    {
        keyboxOpen.SetActive(true);
        foreach (TMP_InputField inputfield in inputfields)
        {
            inputfield.interactable = false;
        }
    }
}
