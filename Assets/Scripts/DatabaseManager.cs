using System.Collections.Generic;
using UnityEngine;
using static Define;

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
    private Dictionary<TrickName, bool> trickStatus_Kid = new Dictionary<TrickName, bool>()
    {
        { TrickName.Bear,           false },
        { TrickName.Clock,          false },
        { TrickName.Console,        false },
        { TrickName.ConsoleHole,    false },
        { TrickName.Curtain,        false },
        { TrickName.Door,           false },
        { TrickName.DrawerZoom,     false },
        { TrickName.FamilyPicture,  false },
        { TrickName.Lamp,           false },
        { TrickName.LegoHole,       false },
        { TrickName.LegoHole1,      false },
        { TrickName.LegoHole2,      false },
        { TrickName.LegoHole3,      false },
        { TrickName.Safe,           false },
        { TrickName.Switch,         false },
        { TrickName.WorldMap,       false },
    };

    // ���̵��� Ʈ�� ���� ��Ȳ
    private Dictionary<TrickName, bool> trickStatus_Idol = new Dictionary<TrickName, bool>()
    {
        { TrickName.Bed,                false },
        { TrickName.Closet,             false },
        { TrickName.DressingTable,      false },
        { TrickName.LaptopBackground,   false },
        { TrickName.LaptopPassword,     false },
        { TrickName.MusicBox,           false },
        { TrickName.MusicPlateZoom,     false },
        { TrickName.Locker,             false },
        { TrickName.Table,              false },
        { TrickName.Poster,             false },
    };

    // �Ž� Ʈ�� ���� ��Ȳ
    private Dictionary<TrickName, bool> trickStatus_Living = new Dictionary<TrickName, bool>()
    {
        { TrickName.CardReader,         false },
        { TrickName.Carpet,             false },
        { TrickName.Hatch,              false },
        { TrickName.CoinMachine,        false },
    };

    // �Ž� ��ġ �ɼ� ���� ��ȣ
    public Dictionary<ItemName, int> trickStatus_Hatch = new Dictionary<ItemName, int>()
    { 
        {ItemName.Latch0, -1 },
        {ItemName.Latch1, -1 },
        {ItemName.Latch2, -1 },
        {ItemName.Latch3, -1 },

    };

    // �������� Ʈ�� ���� ��Ȳ
    private Dictionary<TrickName, bool> trickStatus_Researcher = new Dictionary<TrickName, bool>()
    {
        { TrickName.Cabinet,            false },
        { TrickName.DrawerLocker1,      false },
        { TrickName.DrawerLocker2,      false },
        { TrickName.Stand,              false },
        { TrickName.RCloset,            false },
        { TrickName.Clothes,            false },

    };

    // CEO�� Ʈ�� ���� ��Ȳ
    private Dictionary<TrickName, bool> trickStatus_CEO = new Dictionary<TrickName, bool>()
    {
        { TrickName.Deer,               false },
        { TrickName.DrawerCEO,          false },
        { TrickName.SafeCEO,            false },
        { TrickName.CubePuzzle,         false },
        { TrickName.EmptySlot0,         false },
        { TrickName.EmptySlot1,         false },
        { TrickName.EmptySlot2,         false },
        { TrickName.Parrot,             false },
        { TrickName.Sofa,               false },
        { TrickName.Lion,               false },
        { TrickName.Book,               false },
    };

    // Dictionary�� Ʈ���� �����ϳ� Ȯ��
    public bool IsTrickExist(RoomName roomName, TrickName trickName)
    {
        switch (roomName)
        {
            case RoomName.Kid:
                return trickStatus_Kid.ContainsKey(trickName);
            case RoomName.Idol:
                return trickStatus_Idol.ContainsKey(trickName);
            case RoomName.Living:
                return trickStatus_Living.ContainsKey(trickName);
            case RoomName.Researcher:
                return trickStatus_Researcher.ContainsKey(trickName);
            case RoomName.CEO:
                return trickStatus_CEO.ContainsKey(trickName);
            default:
                return true;
        }
    }
    // Ʈ���� Ǯ�ȴ°� Ȯ��
    public bool IsTrickSolved(RoomName roomName, TrickName trickName)
    {
        switch (roomName)
        {
            case RoomName.Kid:
                return trickStatus_Kid[trickName];
            case RoomName.Idol:
                return trickStatus_Idol[trickName];
            case RoomName.Living:
                return trickStatus_Living[trickName];
            case RoomName.Researcher:
                return trickStatus_Researcher[trickName];
            case RoomName.CEO:
                return trickStatus_CEO[trickName];
            default:
                return false;
        }
    }

    // Ʈ���� ���� ����
    public void SetTrickStatus(RoomName roomName, TrickName trickName, bool status)
    {
        Debug.LogFormat("{0} ���� {1} Ʈ���� {2} �Ǿ��ٰ� ����", roomName, trickName, status);

        switch (roomName)
        {
            case RoomName.Kid:
                trickStatus_Kid[trickName] = status;
                break;
            case RoomName.Idol:
                trickStatus_Idol[trickName] = status;
                break;
            case RoomName.Living:
                trickStatus_Living[trickName] = status;
                break;            
            case RoomName.Researcher:
                trickStatus_Researcher[trickName] = status;
                break;
            case RoomName.CEO:
                trickStatus_CEO[trickName] = status;
                break;
            default:
                break;
        }
    }

    // ���̹� ������ ȹ�� ��Ȳ
    private Dictionary<ItemName, bool> isItemAcquired_Kid = new Dictionary<ItemName, bool>()
    {
        {ItemName.Console,      false},
        {ItemName.Cutter,       false},
        {ItemName.KidRoomKey,   false},
        {ItemName.Latch0,       false},
        {ItemName.Lego1,        false},
        {ItemName.Lego2,        false},
        {ItemName.Lego3,        false},
        {ItemName.Password,     false},
    };
    // ���̵��� ������ ȹ�� ��Ȳ
    private Dictionary<ItemName, bool> isItemAcquired_Idol = new Dictionary<ItemName, bool>()
    {
        {ItemName.Broom,        false},
        {ItemName.Latch1,       false},
        {ItemName.MusicBox,     false},
    };

    // �Ž� ������ ȹ�� ��Ȳ
    private Dictionary<ItemName, bool> isItemAcquired_Living = new Dictionary<ItemName, bool>()
    {
        {ItemName.AccessCard,   false},
    };

    // �������� ������ ȹ�� ��Ȳ
    private Dictionary<ItemName, bool> isItemAcquired_Researcher = new Dictionary<ItemName, bool>()
    {
        {ItemName.TestTubeRed,      false},
        {ItemName.TestTubeYellow,   false},
        {ItemName.TestTubeBlue,     false},
        {ItemName.GoldKey,          false},
        {ItemName.Magnifier,        false},
        {ItemName.Coins,            false},
        {ItemName.Latch2,           false},
    };

    // CEO�� ������ ȹ�� ��Ȳ
    private Dictionary<ItemName, bool> isItemAcquired_CEO = new Dictionary<ItemName, bool>()
    {
        {ItemName.Gun,              false},
        {ItemName.CubePurple,        false},
        {ItemName.CubeBlue,         false},
        {ItemName.CubeRed,          false},
        {ItemName.CubeYellow,       false},
        {ItemName.Latch3,           false},
        {ItemName.DeadParrot,       false},
    };

    public bool IsItemAcquired(RoomName roomName, ItemName itemName)
    {
        switch (roomName)
        {
            case RoomName.Kid:
                return isItemAcquired_Kid[itemName];
            case RoomName.Idol:
                return isItemAcquired_Idol[itemName];
            case RoomName.Living:
                return isItemAcquired_Living[itemName];
            case RoomName.Researcher:
                return isItemAcquired_Researcher[itemName];
            case RoomName.CEO:
                return isItemAcquired_CEO[itemName];
            default:
                return false;
        }
    }
    public void SetItemAcquired(RoomName roomName, ItemName itemName)
    {
        Debug.LogFormat("{0} ���� {1} �������� ȹ���Ͽ��ٰ� ����", roomName, itemName);

        switch (roomName)
        {
            case RoomName.Kid:
                isItemAcquired_Kid[itemName] = true;
                break;
            case RoomName.Idol:
                isItemAcquired_Idol[itemName] = true;
                break;
            case RoomName.Living:
                isItemAcquired_Living[itemName] = true;
                break;            
            case RoomName.Researcher:
                isItemAcquired_Researcher[itemName] = true;
                break;
            case RoomName.CEO:
                isItemAcquired_CEO[itemName] = true;
                break;
            default:
                break;
        }
    }
}
