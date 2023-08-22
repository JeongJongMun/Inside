using TMPro;
using UnityEngine;

public class IdolRoomLaptopPassword : Trick
{
    [Header("비밀번호 입력창")]
    public TMP_InputField passwordInputField;

    [Header("곰돌이 배경화면 스크립트 참조")]
    public LaptopBackground script;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (passwordInputField.text == "idol") 
            {
                Debug.LogFormat("{0} Solved", this.name);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                passwordInputField.text = "";
            }
        }
    }

    public override void SolvedAction()
    {
        script.isEyesMoving = true;
        gameObject.SetActive(false);
    }
}
