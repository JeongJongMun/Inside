using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidRoomManager : RoomManager
{

    public void AddTrick(Trick trick)
    {
        if (tricks.Contains(trick)) 
        {
            Debug.Log("이미 해당 트릭이 리스트에 존재하고 있음.");
        }
        else
        {
            tricks.Add(trick);
        }
    }
    public void RemoveTrick(Trick trick)
    {
        if (tricks.Contains(trick))
        {
            tricks.Remove(trick);
        }
        else
        {
            Debug.Log("해당 트릭이 리스트에 존재하지 않아서 제거하지 못함.");
        }
    }
    // 트릭 클릭 시 트릭들에게 알림



    /*
    아이방 트릭
    1) 벽면 1
    1-1. 시계 덜렁 : 시계 시침 맞출 시
    1-2. 곰돌이 머리 잘림 : 커터칼로 상호작용
    1-3. 서랍장 잠김 : 열쇠로 열때

    2) 벽면 2
    2-1. 가족 사진 잘림 : 커터칼로 상호작용

    3) 벽면 3
    3-1. 세계지도 잘림 : 커터칼로 상호작용

    4) 벽면 4
    4-1. 서재 레고 배치 : 레고 상호작용
    4-2. 서재 게임기 배치 : 게임기 상호작용
    4-3. 서재 밀림 : 게임기 게임 성공
     */
}
