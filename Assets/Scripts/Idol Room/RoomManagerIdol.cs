using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        SoundManager.instance.SFXPlay("drawerOpened");
        desk.SetActive(false);
    }
    public void OnClickDoor()
    {
        if (DatabaseManager.Instance.GetData(Define.TrickName.MusicPlateZoom))
        {
            SoundManager.instance.SFXPlay("doorOpened");
            StartCoroutine(LoadHallway());
        }
        else
        {
            // 잠김 소리 재생
            SoundManager.instance.SFXPlay("doorLocked");
        }
    }

    public void OnclickTable()
    {
        if (DatabaseManager.Instance.GetData(Define.TrickName.DressingTable)) return;
        // beep--- sound play
        SoundManager.instance.SFXPlay("deadheart");
    }

    private IEnumerator LoadHallway()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Hallway");
    }
    
}