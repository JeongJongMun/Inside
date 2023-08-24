using System.Collections.Generic;
using UnityEngine;
using static Define;
using PlayFab;
using PlayFab.ClientModels;

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

    // Ʈ�� ���� ��Ȳ
    private Dictionary<TrickName, bool> trickStatus = new Dictionary<TrickName, bool>()
    {
        // ���̹�
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

        // ���̵��� 
        { TrickName.Bed,                false },
        { TrickName.Closet,             false },
        { TrickName.DressingTable,      false },
        { TrickName.LaptopPassword,     false },
        { TrickName.MusicBox,           false },
        { TrickName.MusicPlateZoom,     false },
        { TrickName.Locker,             false },
        { TrickName.Table,              false },
        { TrickName.Poster,             false },

        // �Ž�
        { TrickName.CardReader,         false },
        { TrickName.Carpet,             false },
        { TrickName.Hatch,              false },
        { TrickName.CoinMachine,        false },

        // ��������
        { TrickName.Cabinet,            false },
        { TrickName.DrawerLocker1,      false },
        { TrickName.DrawerLocker2,      false },
        { TrickName.Stand,              false },
        { TrickName.RCloset,            false },
        { TrickName.Clothes,            false },

        // CEO��
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

        // �����ڹ�
        { TrickName.Keybox,             false },
        { TrickName.CabinetKiller,      false },
        { TrickName.HangerHole,         false },
        { TrickName.HangerInput,        false },
        { TrickName.PostIt1,            false },
        { TrickName.PostIt2,            false },
        { TrickName.LockerKiller,       false },

    };

    // �Ž� ��ġ �ɼ� ���� ��ȣ
    public Dictionary<ItemName, int> trickStatus_Hatch = new Dictionary<ItemName, int>()
    { 
        {ItemName.Latch0, -1 },
        {ItemName.Latch1, -1 },
        {ItemName.Latch2, -1 },
        {ItemName.Latch3, -1 },
    };


    // Dictionary�� Ʈ���� �����ϳ� Ȯ��
    public bool IsTrickExist(TrickName trickName)
    {
        return trickStatus.ContainsKey(trickName);
    }
    // Ʈ���� Ǯ�ȴ°� Ȯ��
    public bool IsTrickSolved(TrickName trickName)
    {
        return trickStatus[trickName];
    }

    // Ʈ���� ���� ����
    public void SetTrickStatus(TrickName trickName, bool status)
    {
        trickStatus[trickName] = status;
        Debug.LogFormat("{0} Ʈ���� {1} �Ǿ��ٰ� ����", trickName, status);
    }


    // ������ ȹ�� ��Ȳ
    private Dictionary<ItemName, bool> itemStatus = new Dictionary<ItemName, bool>()
    {
        // ���̹�
        {ItemName.Console,          false},
        {ItemName.Cutter,           false},
        {ItemName.KidRoomKey,       false},
        {ItemName.Latch0,           false},
        {ItemName.Lego1,            false},
        {ItemName.Lego2,            false},
        {ItemName.Lego3,            false},
        {ItemName.Password,         false},

        // ���̵���
        {ItemName.Broom,            false},
        {ItemName.Latch1,           false},
        {ItemName.MusicBox,         false},

        // �Ž�
        {ItemName.AccessCard,       false},

        // ��������
        {ItemName.TestTubeRed,      false},
        {ItemName.TestTubeYellow,   false},
        {ItemName.TestTubeBlue,     false},
        {ItemName.GoldKey,          false},
        {ItemName.Magnifier,        false},
        {ItemName.Coins,            false},
        {ItemName.Latch2,           false},

        // CEO��
        {ItemName.Gun,              false},
        {ItemName.CubePurple,       false},
        {ItemName.CubeBlue,         false},
        {ItemName.CubeRed,          false},
        {ItemName.CubeYellow,       false},
        {ItemName.Latch3,           false},
        {ItemName.DeadParrot,       false},

        // �����ڹ�
        {ItemName.Hanger,           false},
        {ItemName.ClosetKey,        false},
        {ItemName.Medicine,         false},
        {ItemName.Pencil1,          false},
        {ItemName.Pencil2,          false},
    };

    public bool IsItemAcquired(ItemName itemName)
    {
        return itemStatus[itemName];
    }
    public void SetItemAcquired(ItemName itemName)
    {
        itemStatus[itemName] = true;
        Debug.LogFormat("{0} �������� ȹ���Ͽ��ٰ� ����", itemName);
    }
}
