using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class KillerRoomKeybox : Trick
{
    [Header("InputField들")]
    public TMP_InputField[] inputfields;

    [Header("Keybox 비밀번호 입력들")]
    public TMP_Text[] inputs;

    [SerializeField]
    [Header("비밀번호 정답")]
    private List<string> answers = new List<string>()
    {
        "w", "a", "n", "t"
    };

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
            if (inputs[0].text.Equals("w") && inputs[1].text.Equals("a") && inputs[2].text.Equals("n") && inputs[3].text.Equals("t"))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                Debug.LogFormat("{0}|{1}|{2}|{3}", inputs[0].text, inputs[1].text, inputs[2].text, inputs[3].text);
            }
        }
    }


    public override void SolvedAction()
    {
        keyboxOpen.SetActive(true);
    }
}
