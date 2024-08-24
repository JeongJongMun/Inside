using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RoomManagerIdol : RoomManager
{
    public MusicPlate musicPlate;

    public void OnClickDoor()
    {
        if (DatabaseManager.Instance.GetData(Define.TrickName.MusicPlate))
        {
            SoundManager.instance.SFXPlay("doorOpen");
            StartCoroutine(LoadHallway());
        }
        else
        {
            SoundManager.instance.SFXPlay("doorLocked");
        }
    }

    private IEnumerator LoadHallway()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Hallway");
    }
}