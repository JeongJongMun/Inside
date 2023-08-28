using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;

public class PlayFabGetAccountInfo : MonoBehaviour
{
    [Header("유저 정보 텍스트")]
    public TMP_Text accountInfo;

    [Header("불러오기 버튼")]
    public Button loadButton;
    void Start()
    {
        GetUserAccountInfo();
        GetUserData();
    }

    // PlayFab에서 유저 계정 정보를 가져오는 함수
    private void GetUserAccountInfo()
    {
        var userDataRequest = new GetAccountInfoRequest();

        PlayFabClientAPI.GetAccountInfo(userDataRequest, OnGetUserAccountSuccess, OnGetUserAccountFailure);
    }

    // 유저 계정 정보 가져오기 성공 콜백
    private void OnGetUserAccountSuccess(GetAccountInfoResult result)
    {
        Debug.Log("유저 계정 정보 가져오기 성공");
        // 화면에 Username, Playfab ID 띄우기
        accountInfo.text = result.AccountInfo.Username + "\n" + result.AccountInfo.PlayFabId;
        // Playfab ID 저장
        DatabaseManager.Instance.playfabID = result.AccountInfo.PlayFabId;
    }

    // 유저 계정 정보 가져오기 실패 콜백
    private void OnGetUserAccountFailure(PlayFabError error)
    {
        Debug.LogError("유저 계정 정보 가져오기 실패: " + error.GenerateErrorReport());
    }

    // DB에 저장된 정보에 따라 불러오기 버튼 활성화/비활성화 함수
    private void GetUserData()
    {
        var request = new GetUserDataRequest() { PlayFabId = DatabaseManager.Instance.playfabID };
        PlayFabClientAPI.GetUserData(request, (result) =>
        {
            bool canLoad = true;

            foreach (var eachData in result.Data)
            {
                if ((eachData.Key.Contains("TrickContent") || eachData.Key.Contains("ItemContent") || eachData.Key.Contains("InventoryContent")) &&
                    eachData.Value.Value == null)
                {
                    canLoad = false;
                    break;
                }
            }

            loadButton.interactable = canLoad;
        }, (error) =>
        {
            Debug.LogError("error : " + error.GenerateErrorReport());
        });
    }
}
