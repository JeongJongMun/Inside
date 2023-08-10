using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    // 게임 내에 DatabaseManager 인스턴스는 이 instance에 담긴 녀석만 존재
    // 보안을 위해 private
    private static DatabaseManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에 파괴 X
        }
        else Destroy(gameObject);
    }

    // DatabaseManager 인스턴스에 접근하는 프로퍼티
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

    // 아이방 트릭 진행 상황
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

    // 아이돌방 트릭 진행 상황
    private Dictionary<string, bool> trickStatus_Idol = new Dictionary<string, bool>()
    {
        //{ "Closet",     false},
    };

    // Dictionary에 트릭이 존재하나 확인
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
    // 트릭이 풀렸는가 확인
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
    // 트릭을 상태 설정
    public void SetTrickStatus(string roomName, string trickName, bool status)
    {
        Debug.LogFormat("{0} 방의 {1} 트릭이 {2} 되었다고 저장", roomName, trickName, status);

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

    // 아이방 아이템 획득 상황
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
    // 아이돌방 아이템 획득 상황
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
                Debug.LogFormat("{0} 방의 {1} 아이템을 획득하였다고 저장", roomName, itemName);
                break;
            case "Idol":
                isItemAcquired_Idol[itemName] = true;
                Debug.LogFormat("{0} 방의 {1} 아이템을 획득하였다고 저장", roomName, itemName);
                break;
            default:
                break;
        }
    }
}
