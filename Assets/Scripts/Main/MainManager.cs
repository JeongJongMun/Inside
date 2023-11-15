using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using System.Collections;
using TMPro;

public class MainManager : MonoBehaviour
{
    [Header("���� �г�")]
    public GameObject guidancePanel;

    [Header("�Ѿ�� �ؽ�Ʈ")]
    public TMP_Text skipText;

    [Header("��� �ð�")]
    public float waitTime;


    private void Start()
    {
        guidancePanel.SetActive(false); // ������ �� �г��� ��Ȱ��ȭ
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
            // ��ġ ��ٸ���
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

    // �����ϱ� ��ư Ŭ�� �� DB �ʱ�ȭ & �κ��丮 �ʱ�ȭ
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


    // �ҷ����� ��ư Ŭ�� �� DB�� ����� Ʈ��/������ ���� �ҷ�����
    public void OnClickLoadGameBtn()
    { 
        Inventory.Instance.ClearInventory();
        DatabaseManager.Instance.GetUserData();
        GameManager.Instance.UICanvasSetActive();
        SceneManager.LoadScene("KidRoom");
    }

    // �α׾ƿ� ��ư Ŭ�� ��
    public void OnClickLogOutBtn()
    {
        PlayFabClientAPI.ForgetAllCredentials(); // �α��� �ڰ� ���� �����
        SceneManager.LoadScene("SignIn");
    }
}
