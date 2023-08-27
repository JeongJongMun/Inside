using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayFabSignIn : MonoBehaviour
{
    [Header("이메일 입력창")]
    public TMP_InputField emailInputField;

    [Header("비밀번호 입력창")]
    public TMP_InputField passwordInputField;

    [Header("에러 메시지 UI")]
    public TMP_Text errorMessageText;

    SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    public void SignIn()
    {
        SoundManager.instance.SFXPlay("buttonSound"); // 효과음
        var emailRequest = new LoginWithEmailAddressRequest
        {
            Email = emailInputField.text,
            Password = passwordInputField.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(emailRequest, OnSignInSuccess, OnSignInFailure);
    }
    private void OnSignInSuccess(LoginResult result)
    {
        SceneManager.LoadScene("Main");
    }
    private void OnSignInFailure(PlayFabError error)
    {
        Debug.LogWarning(error);
        errorMessageText.text = "로그인에 실패하였습니다.\n" + error.ToString().Substring(30);
    }
    public void ClickSignUpBtn()
    {
        SoundManager.instance.SFXPlay("buttonSound"); // 효과음
        StartCoroutine(_ClickSignUpBtn());
    }

    private IEnumerator _ClickSignUpBtn()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("SignUp");
    }

}
