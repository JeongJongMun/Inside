using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using System.Collections;

public class MainManager : MonoBehaviour
{
    [Header("�����ϱ� ���� �г�")]
    public GameObject newgameHelpPanel;

    [Header("������ �ߴ� �ð�")]
    public float helpTime;


    private void Start()
    {
        newgameHelpPanel.SetActive(false); // ������ �� �г��� ��Ȱ��ȭ
    }

    private IEnumerator OnHelp()
    {
        newgameHelpPanel.SetActive(true);
        yield return new WaitForSeconds(helpTime);
        newgameHelpPanel.SetActive(false);
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
