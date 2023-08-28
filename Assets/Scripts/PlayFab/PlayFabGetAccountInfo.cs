using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;

public class PlayFabGetAccountInfo : MonoBehaviour
{
    [Header("���� ���� �ؽ�Ʈ")]
    public TMP_Text accountInfo;

    [Header("�ҷ����� ��ư")]
    public Button loadButton;
    void Start()
    {
        GetUserAccountInfo();
        GetUserData();
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
        // ȭ�鿡 Username, Playfab ID ����
        accountInfo.text = result.AccountInfo.Username + "\n" + result.AccountInfo.PlayFabId;
        // Playfab ID ����
        DatabaseManager.Instance.playfabID = result.AccountInfo.PlayFabId;
    }

    // ���� ���� ���� �������� ���� �ݹ�
    private void OnGetUserAccountFailure(PlayFabError error)
    {
        Debug.LogError("���� ���� ���� �������� ����: " + error.GenerateErrorReport());
    }

    // DB�� ����� ������ ���� �ҷ����� ��ư Ȱ��ȭ/��Ȱ��ȭ �Լ�
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
