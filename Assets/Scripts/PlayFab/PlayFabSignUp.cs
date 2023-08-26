using PlayFab;
using PlayFab.ClientModels;
using System.Net.Mail;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayFabSignUp : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;
    public TMP_InputField passwordCheckInputField;

    public void SignUp()
    {
        SoundManager.instance.SFXPlay("buttonSound");
        // @�� �������� ���� ���ڿ��� ����
        string username = emailInputField.text.Split(new[] { '@' })[0];

        // ȸ�����Կ� �ʿ��� ���� �Է�
        var emailRequest = new RegisterPlayFabUserRequest
        {
            Username = username,
            Email = emailInputField.text, 
            Password = passwordInputField.text
        };

        // ��й�ȣ Ȯ��
        if (passwordInputField.text != passwordCheckInputField.text)
        {
            Debug.LogWarning("��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            return;
        }
        // ��й�ȣ �ڸ��� üũ
        if (passwordInputField.text.Length < 8)
        {
            Debug.LogWarning("��й�ȣ�� 8�ڸ� �̻� �Է��ϼ���.");
            return;
        }

        // ȸ������
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
        Debug.LogWarning("ȸ������ ����");
        Debug.LogWarning(error);
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
