using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManagerIdol : RoomManager
{
    [Header("MusicPlate가 풀리면 문 열림")]
    public IdolRoomMusicPlateZoom musicPlate;
    public void OnClickClosetHole()
    {
        SceneManager.LoadScene("KidRoom");
    }
    public void OnClickDrawer(GameObject desk)
    {
        desk.SetActive(false);
    }
    public void OnClickDoor()
    {
        if (DatabaseManager.Instance.IsTrickSolved(Define.RoomName.Idol, Define.TrickName.MusicPlateZoom))
        {
            SceneManager.LoadScene("Hallway");
        }
        else
        {
            // 잠김 소리 재생
        }
    }
}