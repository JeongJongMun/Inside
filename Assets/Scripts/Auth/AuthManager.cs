using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

/* AuthManager.cs
 * PlayFab 로그인 및 회원가입을 관리하는 스크립트
 */
public class AuthManager : MonoBehaviour 
{
#region Public Variables
    public static AuthManager Instance {
        get {
            if (instance == null) {
                Debug.LogWarning("AuthManager 인스턴스가 존재하지 않습니다.");
                return null;
            }
            return instance;
        }
        private set {
            instance = value;
        }
    }
#endregion

#region Private Variables
    private static AuthManager instance = null;
    private const string INCORRECT_PASSWORD = "비밀번호가 일치하지 않습니다. 다시 입력해주세요.";
    private const string OUT_OF_LENGTH = "비밀번호는 최소 8자 이상이어야 합니다.";
    private const string GET_USER_ACCOUNT_FAILURE = "계정 정보를 불러오는데 실패했습니다.";
#endregion

#region Public Methods
    public void Login(string _email, string _password) 
    {
        var emailRequest = new LoginWithEmailAddressRequest {
            Email = _email,
            Password = _password
        };
        PlayFabClientAPI.LoginWithEmailAddress(emailRequest, OnLoginSuccess, OnLoginFailure);
    }

    public void SignUp(string _email, string _password, string _passwordCheck, ref TMP_Text errorText) 
    {
        var emailRequest = new RegisterPlayFabUserRequest {
            // Username = _email.Split(new[] { '@' })[0],
            Username = _email,
            Email = _email,
            Password = _password
        };

        if (_password != _passwordCheck) {
            MainUI.instance.OnErrorMessage(INCORRECT_PASSWORD);
            return;
        }

        if (_password.Length < 8) {
            MainUI.instance.OnErrorMessage(OUT_OF_LENGTH);
            return;
        }

        PlayFabClientAPI.RegisterPlayFabUser(emailRequest, OnSignUpSuccess, OnSignUpFailure);
    }
#endregion

#region Private Methods
    private void Awake() 
    {
        Instance = this;
    }
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("로그인 성공");
        GetAccountInfo();
        GameManager.Instance.ChangeState(new MainState());
    }
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning(error);
        MainUI.instance.OnErrorMessage(error.ErrorMessage);
    }
    private void OnSignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("회원가입 성공");
        GameManager.Instance.ChangeState(new LoginState());
    }
    private void OnSignUpFailure(PlayFabError error)
    {
        Debug.LogWarning(error);
        MainUI.instance.OnErrorMessage(error.ErrorMessage);
    }
    private void GetAccountInfo()
    {
        var userDataRequest = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(userDataRequest, OnGetUserAccountSuccess, OnGetUserAccountFailure);
    }
    private void OnGetUserAccountSuccess(GetAccountInfoResult result)
    {
        Debug.Log("계정 정보를 성공적으로 불러왔습니다.");

        // TODO: 
        // Playfab ID 저장
        // DatabaseManager.Instance.playfabID = result.AccountInfo.PlayFabId;
        
        var request = new GetUserDataRequest() { 
            PlayFabId = result.AccountInfo.PlayFabId 
        };
        PlayFabClientAPI.GetUserData(request, (result) => {
            bool canLoad = true;
            foreach (var eachData in result.Data) {
                if ((eachData.Key.Contains("TrickContent") || eachData.Key.Contains("ItemContent") || 
                    eachData.Key.Contains("InventoryContent")) && eachData.Value.Value == null) {
                    canLoad = false;
                    break;
                }
            }

            MainUI.instance.loadGameButton.interactable = canLoad;
        }, (error) => {
            Debug.LogError("PlayFabClientAPI.GetUserData Error : " + error.GenerateErrorReport());
            MainUI.instance.OnErrorMessage("PlayFabClientAPI.GetUserData" + error.ErrorMessage);
        });
    
    }
    private void OnGetUserAccountFailure(PlayFabError error)
    {
        Debug.LogError(GET_USER_ACCOUNT_FAILURE + error.GenerateErrorReport());
        MainUI.instance.OnErrorMessage(GET_USER_ACCOUNT_FAILURE);
    }
#endregion
}
