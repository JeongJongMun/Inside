// ----- C#
using System;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

// ----- User Defined
using static Define;

public class Data<T>
{
    public T name;
    public bool status;
}
public class HData
{
    public EItemType Type;
    public int number;
}
public class DatabaseManager : MonoBehaviour
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    // ----- Private
    private static DatabaseManager instance = null;
    private List<Data<ETrickType>> TrickData = new List<Data<ETrickType>>();
    private List<Data<EItemType>> ItemData = new List<Data<EItemType>>();
    private List<HData> HatchData = new List<HData>()
    {
        new HData { Type = EItemType.Latch0, number = -1 },
        new HData { Type = EItemType.Latch1, number = -1 },
        new HData { Type = EItemType.Latch2, number = -1 },
        new HData { Type = EItemType.Latch3, number = -1 },
    };
    private List<EItemType> InventoryData = new List<EItemType>();

    // ----- Public
    public static DatabaseManager Instance { get { return instance; } }
    public string playfabID = string.Empty;
    [HideInInspector] public int MentalPointData = 3;

    // --------------------------------------------------
    // Functions - Event
    // --------------------------------------------------
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    
    // --------------------------------------------------
    // Functions - Normal
    // --------------------------------------------------
    public void SetUserData(Dictionary<string, string> data)
    {
        var request = new UpdateUserDataRequest() { Data = data, Permission = UserDataPermission.Public };
        try
        {
            PlayFabClientAPI.UpdateUserData(request, (result) =>
            {
                Debug.Log("Update Player Data!");
            }, MainUI.instance.OnErrorMessage);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void GetUserData()
    {
        var request = new GetUserDataRequest() { PlayFabId = playfabID };
        PlayFabClientAPI.GetUserData(request, (GetUserDataResult result) =>
        {
            foreach (var eachData in result.Data)
            {
                string key = eachData.Key;

                if (key.Contains("TrickContent"))
                {
                    TrickData = JsonConvert.DeserializeObject<List<Data<ETrickType>>>(eachData.Value.Value);
                }
                else if (key.Contains("ItemContent"))
                {
                    ItemData = JsonConvert.DeserializeObject<List<Data<EItemType>>>(eachData.Value.Value);
                }
                else if (key.Contains("HatchContent"))
                {
                    HatchData = JsonConvert.DeserializeObject<List<HData>>(eachData.Value.Value);
                }
                else if (key.Contains("MentalContent"))
                {
                    MentalPointData = JsonConvert.DeserializeObject<int>(eachData.Value.Value);
                    // InGameManager.instance.MentalRecovery();
                    for (int i = MentalPointData - 1; i < 3; i++)
                    {
                        // InGameManager.instance.MentalBreak();
                    }
                }
                else if (key.Contains("InventoryContent"))
                {
                    List<EItemType> content = JsonConvert.DeserializeObject<List<EItemType>>(eachData.Value.Value);
                    foreach (EItemType item in content)
                    {
                        Inventory.instance.AcquireItem(item);
                    }
                    InventoryData = content;
                }
            }

        }, MainUI.instance.OnErrorMessage);
    }

    public bool GetData<T>(T dataName)
    {
        if (typeof(T) == typeof(ETrickType))
        {
            foreach (Data<ETrickType> trick in TrickData)
            {
                if (EqualityComparer<ETrickType>.Default.Equals(trick.name, (ETrickType)(object)dataName))
                {
                    return trick.status;
                }
            }
        }
        else if (typeof(T) == typeof(EItemType))
        {
            foreach (Data<EItemType> item in ItemData)
            {
                if (EqualityComparer<EItemType>.Default.Equals(item.name, (EItemType)(object)dataName))
                {
                    return item.status;
                }
            }
        }

        return false;
    }

    public void SetData<T>(T dataName)
    {
        List<Data<T>> dataList;

        if (typeof(T) == typeof(ETrickType))
        {
            dataList = TrickData as List<Data<T>>;
        }
        else if (typeof(T) == typeof(EItemType))
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

    public void SetHatchData(EItemType type, int index)
    {
        string str = type.ToString();
        int latchIdx = str[str.Length - 1] - '0';
        HatchData[latchIdx].number = index;
    }

    public EItemType GetHatchData(int num)
    {
        foreach (var item in HatchData)
        {
            if (item.number == num)
            {
                return item.Type;
            }
        }

        return EItemType.None;
    }

    public void SetInventoryData<T>(T name, bool isDelete)
    {
        EItemType eItem = EItemType.None;

        if (name is Item)
        {
            Item _item = name as Item;
            eItem = _item.eItemType;
        }
        else if (name is EItemType)
        {
            eItem = (EItemType)(object)name;
        }

        if (isDelete)
        {
            InventoryData.Add(eItem);
        }
        else
        {
            InventoryData.Remove(eItem);
        }
    }

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

    public void ResetData()
    {
        TrickData.Clear();
        ItemData.Clear();
        InventoryData.Clear();
        MentalPointData = 3;
        // TODO: 멘탈 회복
        // InGameManager.Instance.MentalRecovery();
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