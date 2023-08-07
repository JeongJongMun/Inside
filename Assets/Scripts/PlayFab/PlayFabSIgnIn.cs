using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayFabSignIn : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;

    public AudioClip buttonClip; // 효과음 낼 소리

    public void SignIn()
    {
        SoundManager.instance.SFXPlay("Button", buttonClip); // 효과음
        var emailRequest = new LoginWithEmailAddressRequest
        {
            Email = emailInputField.text,
            Password = passwordInputField.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(emailRequest, OnSignInSuccess, OnSignInFailure);
    }
    private void OnSignInSuccess(LoginResult result)
    {
        Debug.LogFormat("�α��� ����");
        SceneManager.LoadScene("Main");
    }
    private void OnSignInFailure(PlayFabError error)
    {
        Debug.LogWarning("�α��� ����");
        Debug.LogWarning(error);
    }
    public void ClickSignUpBtn()
    {
        SoundManager.instance.SFXPlay("Button", buttonClip); // 효과음
        StartCoroutine(_ClickSignUpBtn());
    }

    private IEnumerator _ClickSignUpBtn()
    {
        yield return new WaitForSeconds(buttonClip.length);
        SceneManager.LoadScene("SignUp");
    }

}
