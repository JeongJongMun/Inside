using System.Collections;
using PlayFab;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class OutGameManager : MonoBehaviour
{
#region Private Variables
    private GameManager gameManager;
#endregion

#region Public Variables
    public static OutGameManager instance;
#endregion

#region Private Methods
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private IEnumerator OnHelpPanel()
    {
        MainUI.instance.helpPanel.SetActive(true);
        yield return new WaitForSeconds(1.0f); // TODO: 인게임 로딩 대기
        yield return StartCoroutine(WaitClick());
    }
    private IEnumerator WaitClick()
    {
        TMP_Text targetText = MainUI.instance.touchToStartText;

        targetText.gameObject.SetActive(true);
        
        DOTween.To(() => targetText.color, color => targetText.color = color, new Color(1f, 1f, 1f, 0f), 1.0f).SetLoops(-1, LoopType.Yoyo); // 깜빡임 효과

        yield return new WaitUntil(() => Input.touchCount > 0 || Input.GetMouseButtonDown(0)); // 플레이어 입력 대기

        MainUI.instance.helpPanel.SetActive(false);
        DOTween.Clear(targetText); // 깜빡임 효과 정지
        gameManager.ChangeState(new InGameState());
    }
#endregion

#region Public Methods
    public IEnumerator NewGame()
    {
        yield return StartCoroutine(OnHelpPanel());

        Inventory.instance.ClearInventory();
        DatabaseManager.Instance.ResetData();
        InGameManager.Instance.UICanvasSetActive();
        SceneManager.LoadScene("KidRoom");
    }
    public void LoadGame()
    {
        Inventory.instance.ClearInventory();
        DatabaseManager.Instance.GetUserData();
        InGameManager.Instance.UICanvasSetActive();
        SceneManager.LoadScene("KidRoom");
    }
    public void LogOut()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        GameManager.Instance.ChangeState(new LoginState());
    }
#endregion
}
