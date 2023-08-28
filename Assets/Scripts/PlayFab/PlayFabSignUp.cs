using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayFabSignUp : MonoBehaviour
{
    [Header("이메일 입력창")]
    public TMP_InputField emailInputField;

    [Header("비밀번호 입력창")]
    public TMP_InputField passwordInputField;

    [Header("비밀번호 확인창")]
    public TMP_InputField passwordCheckInputField;

    [Header("에러 메시지 UI")]
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
            errorMessageText.text = "비밀번호가 일치하지 않습니다. 다시 입력해주세요.";
            return;
        }
        // ??й?? ????? ??
        if (passwordInputField.text.Length < 8)
        {
            errorMessageText.text = "비밀번호는 최소 8자리 이상으로 설정해주세요.";
            return;
        }

        // ???????
        PlayFabClientAPI.RegisterPlayFabUser(emailRequest, OnSignUpSuccess, OnSignUpFailire);
    }

    private void OnSignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("회원가입 성공");
         
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
        errorMessageText.text = "회원가입에 실패하였습니다.\n" + error.ToString().Substring(29);
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
