using TMPro;
using UnityEngine;

public class IdolRoomLaptopPassword : Trick
{
    [Header("��й�ȣ �Է�â")]
    public TMP_InputField passwordInputField;

    [Header("������ ���ȭ�� ��ũ��Ʈ ����")]
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
