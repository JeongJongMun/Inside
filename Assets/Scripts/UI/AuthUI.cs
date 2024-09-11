using UnityEngine;
using UnityEngine.UI;
using TMPro;
/* AuthUI.cs
 * 로그인 및 회원가입 UI를 관리하는 스크립트
 */
public class AuthUI : MonoBehaviour
{
#region Public Variables
    public static AuthUI instance;
    // 패널
    public GameObject loginPanel; // 로그인 패널
    public GameObject signUpPanel; // 회원가입 패널

    // 입력 필드
    [Space(30)]
    public TMP_InputField loginEmailField; // 로그인 이메일 입력 필드
    public TMP_InputField loginPasswordField; // 로그인 비밀번호 입력 필드
    public TMP_InputField signUpEmailField; // 회원가입 이메일 입력 필드
    public TMP_InputField signUpPasswordField; // 회원가입 비밀번호 입력 필드
    public TMP_InputField signUpPasswordCheckField; // 회원가입 비밀번호 확인 입력 필드

    // 버튼
    [Space(30)]
    public Button loginButton; // 로그인 버튼
    public Button signUpButton; // 회원가입 버튼
    public Button goToLoginButton; // 로그인 화면으로 이동 버튼
    public Button goToSignUpButton; // 회원가입 화면으로 이동 버튼    
#endregion

#region Private Variables
#endregion

#region Public Methods
    public void ClearInputFields()
    {
        loginEmailField.text = "";
        loginPasswordField.text = "";
        signUpEmailField.text = "";
        signUpPasswordField.text = "";
        signUpPasswordCheckField.text = "";
    }
#endregion

#region Private Methods
    private void Awake()
    {
        instance = this;
        goToLoginButton.onClick.AddListener(() => GameManager.instance.ChangeState(new LoginState()));
        goToSignUpButton.onClick.AddListener(() => GameManager.instance.ChangeState(new SignUpState()));

        loginButton.onClick.AddListener(() => Managers.Auth.Login(loginEmailField.text, loginPasswordField.text));
        signUpButton.onClick.AddListener(() => Managers.Auth.SignUp(signUpEmailField.text, signUpPasswordField.text, signUpPasswordCheckField.text));
        loginPanel.SetActive(false);
        signUpPanel.SetActive(false);
    }
#endregion
}