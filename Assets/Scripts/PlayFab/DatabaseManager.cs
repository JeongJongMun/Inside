using System.Collections.Generic;
using UnityEngine;
using static Define;
using PlayFab;
using PlayFab.ClientModels;
using System;
using Newtonsoft.Json;

public class DatabaseManager : MonoBehaviour
{
    // ���� ���� DatabaseManager �ν��Ͻ��� �� instance�� ��� �༮�� ����
    // ������ ���� private
    private static DatabaseManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ� �ı� X
        }
        else Destroy(gameObject);
    }

    // DatabaseManager �ν��Ͻ��� �����ϴ� ������Ƽ
    public static DatabaseManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    [Header("PlayFab ID")]
    public string playfabID;

    // Ʈ��, ������ ���� ������ Ŭ����
    public class Data<T>
    {
        public T name;
        public bool status;
    }
    // ��ġ�� �ɼ� ���� ������
    public class HData
    {
        public ItemName name;
        public int number;
    }

    private List<Data<TrickName>> TrickData = new List<Data<TrickName>>();
    private List<Data<ItemName>> ItemData = new List<Data<ItemName>>();
    private List<HData> HatchData = new List<HData>()
    {
        new HData { name = ItemName.Latch0, number = -1 },
        new HData { name = ItemName.Latch1, number = -1 },
        new HData { name = ItemName.Latch2, number = -1 },
        new HData { name = ItemName.Latch3, number = -1 },
    };
    private List<ItemName> InventoryData = new List<ItemName>();
    [HideInInspector]
    public int MentalPointData = 3;

    private void DisplayPlayfabError(PlayFabError error) => Debug.LogError("error : " + error.GenerateErrorReport());

    // ���� ������ �����ϱ�
    public void SetUserData(Dictionary<string, string> data)
    {
        var request = new UpdateUserDataRequest() { Data = data, Permission = UserDataPermission.Public };
        try
        {
            PlayFabClientAPI.UpdateUserData(request, (result) =>
            {
                Debug.Log("Update Player Data!");
            }, DisplayPlayfabError);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    // �ҷ����� �� ���� ������ ��������
    public void GetUserData()
    {
        var request = new GetUserDataRequest() { PlayFabId = playfabID };
        PlayFabClientAPI.GetUserData(request, (result) =>
        {
            foreach (var eachData in result.Data)
            {
                string key = eachData.Key;

                if (key.Contains("TrickContent"))
                {
                    TrickData = JsonConvert.DeserializeObject<List<Data<TrickName>>>(eachData.Value.Value);
                }
                else if (key.Contains("ItemContent"))
                {
                    ItemData = JsonConvert.DeserializeObject<List<Data<ItemName>>>(eachData.Value.Value);
                }
                else if (key.Contains("HatchContent"))
                {
                    HatchData = JsonConvert.DeserializeObject<List<HData>>(eachData.Value.Value);
                }
                else if (key.Contains("MentalContent"))
                {
                    MentalPointData = JsonConvert.DeserializeObject<int>(eachData.Value.Value);
                    GameManager.Instance.MentalRecovery();
                    for (int i = MentalPointData - 1; i < 3; i++)
                    {
                        GameManager.Instance.MentalBreak();
                    }
                }
                else if (key.Contains("InventoryContent"))
                {
                    List<ItemName> content = JsonConvert.DeserializeObject<List<ItemName>>(eachData.Value.Value);
                    // �κ��丮�� ������ �߰�
                    foreach (ItemName item in content)
                    {
                        Inventory.Instance.AcquireItem(item);
                    }
                    InventoryData = content;
                }

            }

        }, DisplayPlayfabError);
    }

    // Ʈ���� Ǯ�ȴ°� & �������� ȹ���ߴ°� Ȯ��
    public bool GetData<T>(T dataName)
    {
        if (typeof(T) == typeof(TrickName))
        {
            foreach (Data<TrickName> trick in TrickData)
            {
                if (EqualityComparer<TrickName>.Default.Equals(trick.name, (TrickName)(object)dataName))
                {
                    return trick.status;
                }
            }
        }
        else if (typeof(T) == typeof(ItemName))
        {
            foreach (Data<ItemName> item in ItemData)
            {
                if (EqualityComparer<ItemName>.Default.Equals(item.name, (ItemName)(object)dataName))
                {
                    return item.status;
                }
            }
        }

        return false;
    }

    // Ʈ���� Ǯ�ų� �������� ȹ�� �� ������ ����
    public void SetData<T>(T dataName)
    {
        List<Data<T>> dataList;

        if (typeof(T) == typeof(TrickName))
        {
            dataList = TrickData as List<Data<T>>;
        }
        else if (typeof(T) == typeof(ItemName))
        {
            dataList = ItemData as List<Data<T>>;
        }
        else
        {
            Debug.LogError("Invalid data type.");
            return;
        }

        Data<T> data = new Data<T>();
        data.name = dataName;
        data.status = true;

        dataList.Add(data);
    }

    // ��ġ ������ ����
    public void SetHatchData(ItemName name, int index)
    {
        string str = name.ToString();
        int latchIdx = str[str.Length - 1] - '0';
        HatchData[latchIdx].number = index;
    }

    // ��ġ ������ ��������
    public ItemName GetHatchData(int num)
    {
        foreach (var item in HatchData)
        {
            if (item.number == num)
            {
                return item.name;
            }
        }

        return ItemName.None;
    }

    public void SetInventoryData<T>(T name, bool isDelete)
    {
        ItemName item = ItemName.None;

        if (name is Item)
        {
            Item _item = name as Item;
            item = _item.itemName;
        }
        else if (name is ItemName)
        {
            item = (ItemName)(object)name;
        }

        if (isDelete)
        {
            InventoryData.Add(item);
        }
        else
        {
            InventoryData.Remove(item);
        }
    }

    public bool GetInventoryData(ItemName item)
    {
        foreach (ItemName itemName in InventoryData)
        {
            if (itemName == item)
            {
                return true;
            }
        }
        return false;
    }

    // ������ �����ϱ� (���� ��ư Ŭ�� ��)
    public void SaveData()
    {
        Dictionary<string, string> trickDic = new Dictionary<string, string>
        {
            { "TrickContent", JsonConvert.SerializeObject(TrickData) }
        };

        SetUserData(trickDic);

        Dictionary<string, string> itemDic = new Dictionary<string, string>
        {
            { "ItemContent", JsonConvert.SerializeObject(ItemData) }
        };

        SetUserData(itemDic);

        Dictionary<string, string> hatchDic = new Dictionary<string, string>
        {
            { "HatchContent", JsonConvert.SerializeObject(HatchData) }
        };

        SetUserData(hatchDic);

        Dictionary<string, string> inventoryDic = new Dictionary<string, string>
        {
            { "InventoryContent", JsonConvert.SerializeObject(InventoryData) }
        };

        SetUserData(inventoryDic);

        Dictionary<string, string> mentalDic = new Dictionary<string, string>
        {
            { "MentalContent", JsonConvert.SerializeObject(MentalPointData) }
        };

        SetUserData(mentalDic);
    }

    // �����ϱ� �� ������ �ʱ�ȭ
    public void ResetData()
    {
        TrickData.Clear();
        ItemData.Clear();
        InventoryData.Clear();
        MentalPointData = 3;
        GameManager.Instance.MentalRecovery();
        Dictionary<string, string> trickDic = new Dictionary<string, string>
        {
            { "TrickContent", "" }
        };
        SetUserData(trickDic);

        Dictionary<string, string> itemDic = new Dictionary<string, string>
        {
            { "ItemContent", "" }
        };
        SetUserData(itemDic);

        Dictionary<string, string> inventoryDic = new Dictionary<string, string>
        {
            { "InventoryContent", "" }
        };
        SetUserData(inventoryDic);

        Dictionary<string, string> mentalDic = new Dictionary<string, string>
        {
            { "MentalContent", "3" }
        };
        SetUserData(mentalDic);

        foreach (HData hData in HatchData) hData.number = -1;
        Dictionary<string, string> hatchDic = new Dictionary<string, string>
        {
            { "HatchContent", JsonConvert.SerializeObject(HatchData) }
        };
        SetUserData(hatchDic);


    }

}
