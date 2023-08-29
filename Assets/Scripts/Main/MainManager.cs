using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using System.Collections;

public class MainManager : MonoBehaviour
{
    [Header("새로하기 도움말 패널")]
    public GameObject newgameHelpPanel;

    [Header("도움말이 뜨는 시간")]
    public float helpTime;


    private void Start()
    {
        newgameHelpPanel.SetActive(false); // 시작할 때 패널을 비활성화
    }

    private IEnumerator OnHelp()
    {
        newgameHelpPanel.SetActive(true);
        yield return new WaitForSeconds(helpTime);
        newgameHelpPanel.SetActive(false);
    }

    // 새로하기 버튼 클릭 시 DB 초기화 & 인벤토리 초기화
    public void OnClickNewGameBtn()
    {
        StartCoroutine(ExecuteNewGame());
    }

    private IEnumerator ExecuteNewGame()
    {
        yield return StartCoroutine(OnHelp());

        Inventory.Instance.ClearInventory();
        DatabaseManager.Instance.ResetData();
        GameManager.Instance.UICanvasSetActive();
        SceneManager.LoadScene("KidRoom");
    }


    // 불러오기 버튼 클릭 시 DB에 저장된 트릭/아이템 정보 불러오기
    public void OnClickLoadGameBtn()
    { 
        Inventory.Instance.ClearInventory();
        DatabaseManager.Instance.GetUserData();
        GameManager.Instance.UICanvasSetActive();
        SceneManager.LoadScene("KidRoom");
    }

    // 로그아웃 버튼 클릭 시
    public void OnClickLogOutBtn()
    {
        PlayFabClientAPI.ForgetAllCredentials(); // 로그인 자격 증명 지우기
        SceneManager.LoadScene("SignIn");
    }
}
