using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RoomManagerIdol : RoomManager
{
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
            SoundManager.instance.SFXPlay("doorOpen");
            StartCoroutine(LoadHallway());
        }
        else
        {
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