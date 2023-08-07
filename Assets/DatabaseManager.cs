using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // ���̹� Ʈ�� ���� ��Ȳ
    private Dictionary<string, bool> trickStatus_Kid = new Dictionary<string, bool>()
    {
        //{ "DrawerZoom",     false},
        //{ "Bear",           false },
        //{ "Clock",          false },
        //{ "Curtain",        false },
        //{ "FamilyPicture",  false },
        //{ "Safe",           false },
        //{ "WorldMap",       false },
        //{ "Lamp",           false },
        //{ "LampZoom",       false },
        //{ "LegoHole",       false },
        //{ "LegoHole1",      false },
        //{ "LegoHole2",      false },
        //{ "LegoHole3",      false },
        //{ "ConsoleHole",    false },
        //{ "Console",        false },
        //{ "Door",           false },
        //{ "Switch",         false },
    };

    // ���̵��� Ʈ�� ���� ��Ȳ
    private Dictionary<string, bool> trickStatus_Idol = new Dictionary<string, bool>()
    {
        //{ "Closet",     false},
    };

    // Dictionary�� Ʈ���� �����ϳ� Ȯ��
    public bool IsTrickExist(string roomName, string trickName)
    {
        switch (roomName)
        {
            case "Kid":
                return trickStatus_Kid.ContainsKey(trickName);
            case "Idol":
                return trickStatus_Idol.ContainsKey(trickName);
            default:
                return true;
        }
    }
    // Ʈ���� Ǯ�ȴ°� Ȯ��
    public bool IsTrickSolved(string roomName, string trickName)
    {
        switch (roomName)
        {
            case "Kid":
                return trickStatus_Kid[trickName];
            case "Idol":
                return trickStatus_Idol[trickName];
            default:
                return false;
        }
    }
    // Ʈ���� ���� ����
    public void SetTrickStatus(string roomName, string trickName, bool status)
    {
        Debug.LogFormat("{0} ���� {1} Ʈ���� {2} �Ǿ��ٰ� ����", roomName, trickName, status);

        switch (roomName)
        {
            case "Kid":
                trickStatus_Kid[trickName] = status;
                break;
            case "Idol":
                trickStatus_Idol[trickName] = status;
                break;
            default:
                break;
        }
    }

    // ���̹� ������ ȹ�� ��Ȳ
    private Dictionary<ItemName, bool> isItemAcquired_Kid = new Dictionary<ItemName, bool>()
    {
        {ItemName.Console,     false},
        {ItemName.Cutter,      false},
        {ItemName.KidRoomKey,  false},
        {ItemName.Latch1,      false},
        {ItemName.Lego1,       false},
        {ItemName.Lego2,       false},
        {ItemName.Lego3,       false},
        {ItemName.Password,    false},
    };
    // ���̵��� ������ ȹ�� ��Ȳ
    private Dictionary<ItemName, bool> isItemAcquired_Idol = new Dictionary<ItemName, bool>()
    {
        {ItemName.Broom,       false},
        {ItemName.Latch2,      false},
        {ItemName.MusicBox,    false},
    };

    public bool IsItemAcquired(string roomName, ItemName itemName)
    {
        switch (roomName)
        {
            case "Kid":
                return isItemAcquired_Kid[itemName];
            case "Idol":
                return isItemAcquired_Idol[itemName];
            default:
                return false;
        }
    }
    public void SetItemAcquired(string roomName, ItemName itemName)
    {
        switch (roomName)
        {
            case "Kid":
                isItemAcquired_Kid[itemName] = true;
                Debug.LogFormat("{0} ���� {1} �������� ȹ���Ͽ��ٰ� ����", roomName, itemName);
                break;
            case "Idol":
                isItemAcquired_Idol[itemName] = true;
                Debug.LogFormat("{0} ���� {1} �������� ȹ���Ͽ��ٰ� ����", roomName, itemName);
                break;
            default:
                break;
        }
    }
}
