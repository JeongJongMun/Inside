using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;

public class MainManager : MonoBehaviour
{

    // �����ϱ� ��ư Ŭ�� �� DB �ʱ�ȭ & �κ��丮 �ʱ�ȭ
    public void OnClickNewGameBtn()
    {
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
