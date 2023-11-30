using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RoomManagerKiller : RoomManager
{
    public void OnClickLadder()
    {
        SoundManager.instance.SFXPlay("stair");
        StartCoroutine(LoadLivingRoom());
    }
    public void OnClickEndingRoomDoor()
    {
        StartCoroutine(LoadEndingRoom());
    }

    public void OnClickMedicine(GameObject medicine)
    {
        medicine.SetActive(false);
    }
    
    private IEnumerator LoadLivingRoom()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("LivingRoom");
    }

    private IEnumerator LoadEndingRoom()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("EndingRoom");
    }
}
