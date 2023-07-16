using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayFabGetAccountInfo : MonoBehaviour
{
    public TMP_Text accountInfo;
    void Start()
    {
        GetUserAccountInfo();
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
        accountInfo.text = result.AccountInfo.Username + "\n" + result.AccountInfo.PlayFabId;
    }

    // 유저 계정 정보 가져오기 실패 콜백
    private void OnGetUserAccountFailure(PlayFabError error)
    {
        Debug.LogError("유저 계정 정보 가져오기 실패: " + error.GenerateErrorReport());
    }

}
