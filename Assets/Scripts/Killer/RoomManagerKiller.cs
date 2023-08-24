using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManagerKiller : RoomManager
{
    public void OnClickLadder()
    {
        SceneManager.LoadScene("LivingRoom");
    }
    public void OnClickEndingRoomDoor()
    {
        Debug.Log("엔딩방 입장");
    }
}
