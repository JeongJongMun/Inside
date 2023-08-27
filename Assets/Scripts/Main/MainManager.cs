using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;

public class MainManager : MonoBehaviour
{

    // �����ϱ� ��ư Ŭ�� �� DB �ʱ�ȭ
    public void OnClickNewGameBtn()
    {
        DatabaseManager.Instance.ResetData();
        GameManager.Instance.UICanvasSetActive();
        SceneManager.LoadScene("KidRoom");
    }

    // �ҷ����� ��ư Ŭ�� �� DB�� ����� Ʈ��/������ ���� �ҷ�����
    public void OnClickLoadGameBtn()
    {
        DatabaseManager.Instance.GetUserData();
        GameManager.Instance.UICanvasSetActive();
        SceneManager.LoadScene("KidRoom");
    }

    // ���� ��ư Ŭ�� ��
    public void OnClickSettingBtn()
    {

    }

    // �α׾ƿ� ��ư Ŭ�� ��
    public void OnClickLogOutBtn()
    {
        PlayFabClientAPI.ForgetAllCredentials(); // �α��� �ڰ� ���� �����
        SceneManager.LoadScene("SignIn");
    }
}
