using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayFabSignIn : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;

    public void SignIn()
    {
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
        SceneManager.LoadScene("MainScene");
    }
    private void OnSignInFailure(PlayFabError error)
    {
        Debug.LogWarning("�α��� ����");
        Debug.LogWarning(error);
    }
    public void ClickSignUpBtn()
    {
        SceneManager.LoadScene("SignUpScene");
    }

}
