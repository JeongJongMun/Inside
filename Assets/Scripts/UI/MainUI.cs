using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
/* MainUI.cs
 * 메인 화면 UI를 관리하는 스크립트
 */
public class MainUI : MonoBehaviour
{
#region Public Variables
    public static MainUI instance;
    // 패널
    public GameObject mainPanel; // 메인 패널
    public GameObject helpPanel; // 도움말 패널

    // 버튼
    [Space(30)]
    public Button newGameButton; // 새로하기 버튼
    public Button loadGameButton; // 불러오기 버튼
    public Button logOutButton; // 로그아웃 버튼

    // 팝업 및 텍스트
    [Space(30)]
    public GameObject errorPopup; // 에러 팝업
    public TMP_Text errorMessageText; // 에러 메시지 텍스트
    public TMP_Text touchToStartText; // 터치하여 시작 텍스트
#endregion

#region Private Variables
    private OutGameManager outGameManager;
#endregion

#region Public Methods
    public void OnErrorMessage(string _message)
    {
        errorPopup.SetActive(true);
        errorMessageText.text = _message;
    }
    public void OnErrorMessage(PlayFabError error)
    {
        errorPopup.SetActive(true);
        errorMessageText.text = error.ErrorMessage;
    }
#endregion

#region Private Methods
    private void Awake()
    {
        instance = this;
        mainPanel.SetActive(false);
        helpPanel.SetActive(false);
    }
    private void Start()
    {
        outGameManager = OutGameManager.instance;
        newGameButton.onClick.AddListener(() => StartCoroutine(outGameManager.NewGame()));
        loadGameButton.onClick.AddListener(outGameManager.LoadGame);
        logOutButton.onClick.AddListener(outGameManager.LogOut);
    }
#endregion
}
