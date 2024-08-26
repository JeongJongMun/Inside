using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoomManagerResearcher : RoomManager
{
    public void OnClickDoor()
    {
        SoundManager.instance.SFXPlay("doorOpen");
        StartCoroutine(ForPlaySFX());
    }
    private IEnumerator ForPlaySFX()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("LivingRoom");
    }
}
