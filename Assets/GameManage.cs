using PlayFab.EconomyModels;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    Dictionary<string, bool> trick_kid = new Dictionary<string, bool>()
    {

        /*
        아이방 트릭
        1) 벽면 1
        1-1. 시계 덜렁
        1-2. 곰돌이 머리 잘림
        1-3. 서랍장 잠김

        2) 벽면 2
        2-1. 가족 사진 잘림

        3) 벽면 3
        3-1. 세계지도 잘림

        4) 벽면 4
        4-1. 서재 레고 배치
        4-2. 서재 게임기 배치
        4-3. 서재 밀림
         */

        {"clock", false}, 
        {"bear", false },
        {"drawer", false },
        {"familyPicture", false },
        {"worldMap", false },
        {"legoInLibrary", false },
        {"LibraryOpen", false }
    };

    // 게임 내에 GameManage 인스턴스는 이 instance에 담긴 녀석만 존재
    // 보안을 위해 private
    private static GameManage instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에 파괴 X
        }
        else Destroy(gameObject);
    }

    // GameManage 인스턴스에 접근하는 프로퍼티
    public static GameManage Instance
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
    /*
    필요한 전역 변수
    인벤토리, 정신력 포인트
    게임 진척도(어떤 트릭을 풀었는지)
    

     
     */




}
