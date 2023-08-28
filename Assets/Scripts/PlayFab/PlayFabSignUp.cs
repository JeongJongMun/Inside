using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayFabSignUp : MonoBehaviour
{
    [Header("�̸��� �Է�â")]
    public TMP_InputField emailInputField;

    [Header("��й�ȣ �Է�â")]
    public TMP_InputField passwordInputField;

    [Header("��й�ȣ Ȯ��â")]
    public TMP_InputField passwordCheckInputField;

    [Header("���� �޽��� UI")]
    public TMP_Text errorMessageText;

    public void SignUp()
    {
        SoundManager.instance.SFXPlay("buttonSound");
        // @?? ???????? ???? ??????? ????
        string username = emailInputField.text.Split(new[] { '@' })[0];

        // ???????? ????? ???? ???
        var emailRequest = new RegisterPlayFabUserRequest
        {
            Username = username,
            Email = emailInputField.text, 
            Password = passwordInputField.text
        };

        if (passwordInputField.text != passwordCheckInputField.text)
        {
            errorMessageText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�. �ٽ� �Է����ּ���.";
            return;
        }
        // ??��?? ????? ??
        if (passwordInputField.text.Length < 8)
        {
            errorMessageText.text = "��й�ȣ�� �ּ� 8�ڸ� �̻����� �������ּ���.";
            return;
        }

        // ???????
        PlayFabClientAPI.RegisterPlayFabUser(emailRequest, OnSignUpSuccess, OnSignUpFailire);
    }

    private void OnSignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("ȸ������ ����");
         
        StartCoroutine(_ClickBackBtn());
    }

    private IEnumerator _OnSignUpSuccess()
    {
        
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("SignIn");
    }

    private void OnSignUpFailire(PlayFabError error)
    {
        Debug.LogWarning(error);
        errorMessageText.text = "ȸ�����Կ� �����Ͽ����ϴ�.\n" + error.ToString().Substring(29);
    }
    public void ClickBackBtn()
    {
        SoundManager.instance.SFXPlay("buttonSound");
        StartCoroutine(_ClickBackBtn());
    }

    private IEnumerator _ClickBackBtn()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("SignIn");
    }

}
