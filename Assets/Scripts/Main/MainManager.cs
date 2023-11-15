using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using System.Collections;
using TMPro;

public class MainManager : MonoBehaviour
{
    [Header("도움말 패널")]
    public GameObject guidancePanel;

    [Header("넘어가기 텍스트")]
    public TMP_Text skipText;

    [Header("대기 시간")]
    public float waitTime;


    private void Start()
    {
        guidancePanel.SetActive(false); // 시작할 때 패널을 비활성화
    }

    private IEnumerator BlinkTextEffect(TMP_Text text, float blinkSpeed)
    {
        Color originalColor = text.color;

        while (true)
        {
            float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
    }
    private IEnumerator WaitClick()
    {
        skipText.gameObject.SetActive(true);
        bool clicked = false;

        // Start the blinking effect
        IEnumerator blinkCoroutine = BlinkTextEffect(skipText, 1.0f);
        StartCoroutine(blinkCoroutine);

        while (!clicked)
        {
            // 터치 기다리기
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                guidancePanel.SetActive(false);
                clicked = true;
            }

            yield return null;
        }
    }


    private IEnumerator OnHelp()
    {
        guidancePanel.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(WaitClick());
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
