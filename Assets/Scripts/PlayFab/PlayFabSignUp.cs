using PlayFab;
using PlayFab.ClientModels;
using System.Net.Mail;
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
        // @를 기준으로 왼쪽 문자열만 남김
        string username = emailInputField.text.Split(new[] { '@' })[0];

        // 회원가입에 필요한 정보 입력
        var emailRequest = new RegisterPlayFabUserRequest
        {
            Username = username,
            Email = emailInputField.text, 
            Password = passwordInputField.text
        };

        // 비밀번호 확인
        if (passwordInputField.text != passwordCheckInputField.text)
        {
            Debug.LogWarning("비밀번호가 일치하지 않습니다.");
            return;
        }
        // 비밀번호 자리수 체크
        if (passwordInputField.text.Length < 8)
        {
            Debug.LogWarning("비밀번호를 8자리 이상 입력하세요.");
            return;
        }

        // 회원가입
        PlayFabClientAPI.RegisterPlayFabUser(emailRequest, OnSignUpSuccess, OnSignUpFailire);
    }

    private void OnSignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("회원가입 성공");
        SceneManager.LoadScene("SignInScene");
    }
    private void OnSignUpFailire(PlayFabError error)
    {
        Debug.LogWarning("회원가입 실패");
        Debug.LogWarning(error);
    }
    public void ClickBackBtn()
    {
        SceneManager.LoadScene("SignInScene");
    }


}
