using System.Collections.Generic;
using UnityEngine;
using static Define;
using PlayFab;
using PlayFab.ClientModels;
using System;
using Newtonsoft.Json;
/* DatabaseManager.cs
 * PlayFab 데이터베이스를 관리하는 스크립트
 */
public class Data<T>
{
    public T name;
    public bool status;
}
public class HData
{
    public ItemName name;
    public int number;
}
public class DatabaseManager : MonoBehaviour
{
#region Private Variables
    private static DatabaseManager instance = null;
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
#endregion

#region Public Variables
    public static DatabaseManager Instance { get { return instance; } }
    public string playfabID = string.Empty;
    [HideInInspector] public int MentalPointData = 3;
#endregion

#region Private Methods
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
#endregion

#region Public Methods
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
                    InGameManager.Instance.MentalRecovery();
                    for (int i = MentalPointData - 1; i < 3; i++)
                    {
                        InGameManager.Instance.MentalBreak();
                    }
                }
                else if (key.Contains("InventoryContent"))
                {
                    List<ItemName> content = JsonConvert.DeserializeObject<List<ItemName>>(eachData.Value.Value);
                    foreach (ItemName item in content)
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

    public void SetHatchData(ItemName name, int index)
    {
        string str = name.ToString();
        int latchIdx = str[str.Length - 1] - '0';
        HatchData[latchIdx].number = index;
    }

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
        InGameManager.Instance.MentalRecovery();
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
#endregion
}