using System.Collections.Generic;
using UnityEngine;
using static Define;
using PlayFab;
using PlayFab.ClientModels;

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

    // 트릭 진행 상황
    private Dictionary<TrickName, bool> trickStatus = new Dictionary<TrickName, bool>()
    {
        // 아이방
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

        // 아이돌방 
        { TrickName.Bed,                false },
        { TrickName.Closet,             false },
        { TrickName.DressingTable,      false },
        { TrickName.LaptopPassword,     false },
        { TrickName.MusicBox,           false },
        { TrickName.MusicPlateZoom,     false },
        { TrickName.Locker,             false },
        { TrickName.Table,              false },
        { TrickName.Poster,             false },

        // 거실
        { TrickName.CardReader,         false },
        { TrickName.Carpet,             false },
        { TrickName.Hatch,              false },
        { TrickName.CoinMachine,        false },

        // 연구원방
        { TrickName.Cabinet,            false },
        { TrickName.DrawerLocker1,      false },
        { TrickName.DrawerLocker2,      false },
        { TrickName.Stand,              false },
        { TrickName.RCloset,            false },
        { TrickName.Clothes,            false },

        // CEO방
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

        // 살인자방
        { TrickName.Keybox,             false },
        { TrickName.CabinetKiller,      false },
        { TrickName.HangerHole,         false },
        { TrickName.HangerInput,        false },
        { TrickName.PostIt1,            false },
        { TrickName.PostIt2,            false },
        { TrickName.LockerKiller,       false },

    };

    // 거실 해치 걸쇠 꽂힌 번호
    public Dictionary<ItemName, int> trickStatus_Hatch = new Dictionary<ItemName, int>()
    { 
        {ItemName.Latch0, -1 },
        {ItemName.Latch1, -1 },
        {ItemName.Latch2, -1 },
        {ItemName.Latch3, -1 },
    };


    // Dictionary에 트릭이 존재하나 확인
    public bool IsTrickExist(TrickName trickName)
    {
        return trickStatus.ContainsKey(trickName);
    }
    // 트릭이 풀렸는가 확인
    public bool IsTrickSolved(TrickName trickName)
    {
        return trickStatus[trickName];
    }

    // 트릭의 상태 설정
    public void SetTrickStatus(TrickName trickName, bool status)
    {
        trickStatus[trickName] = status;
        Debug.LogFormat("{0} 트릭이 {1} 되었다고 저장", trickName, status);
    }


    // 아이템 획득 상황
    private Dictionary<ItemName, bool> itemStatus = new Dictionary<ItemName, bool>()
    {
        // 아이방
        {ItemName.Console,          false},
        {ItemName.Cutter,           false},
        {ItemName.KidRoomKey,       false},
        {ItemName.Latch0,           false},
        {ItemName.Lego1,            false},
        {ItemName.Lego2,            false},
        {ItemName.Lego3,            false},
        {ItemName.Password,         false},

        // 아이돌방
        {ItemName.Broom,            false},
        {ItemName.Latch1,           false},
        {ItemName.MusicBox,         false},

        // 거실
        {ItemName.AccessCard,       false},

        // 연구원방
        {ItemName.TestTubeRed,      false},
        {ItemName.TestTubeYellow,   false},
        {ItemName.TestTubeBlue,     false},
        {ItemName.GoldKey,          false},
        {ItemName.Magnifier,        false},
        {ItemName.Coins,            false},
        {ItemName.Latch2,           false},

        // CEO방
        {ItemName.Gun,              false},
        {ItemName.CubePurple,       false},
        {ItemName.CubeBlue,         false},
        {ItemName.CubeRed,          false},
        {ItemName.CubeYellow,       false},
        {ItemName.Latch3,           false},
        {ItemName.DeadParrot,       false},

        // 살인자방
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
        Debug.LogFormat("{0} 아이템을 획득하였다고 저장", itemName);
    }
}
