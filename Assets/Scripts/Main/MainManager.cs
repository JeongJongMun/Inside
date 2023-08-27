using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;

public class MainManager : MonoBehaviour
{

    // 새로하기 버튼 클릭 시 DB 초기화 & 인벤토리 초기화
    public void OnClickNewGameBtn()
    {
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
