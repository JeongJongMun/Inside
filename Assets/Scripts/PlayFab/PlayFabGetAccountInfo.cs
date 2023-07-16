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

    // PlayFab���� ���� ���� ������ �������� �Լ�
    private void GetUserAccountInfo()
    {
        var userDataRequest = new GetAccountInfoRequest();

        PlayFabClientAPI.GetAccountInfo(userDataRequest, OnGetUserAccountSuccess, OnGetUserAccountFailure);
    }

    // ���� ���� ���� �������� ���� �ݹ�
    private void OnGetUserAccountSuccess(GetAccountInfoResult result)
    {
        Debug.Log("���� ���� ���� �������� ����");
        accountInfo.text = result.AccountInfo.Username + "\n" + result.AccountInfo.PlayFabId;
    }

    // ���� ���� ���� �������� ���� �ݹ�
    private void OnGetUserAccountFailure(PlayFabError error)
    {
        Debug.LogError("���� ���� ���� �������� ����: " + error.GenerateErrorReport());
    }

}
