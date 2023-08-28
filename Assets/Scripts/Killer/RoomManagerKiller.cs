using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RoomManagerKiller : RoomManager
{
    public Canvas uiCanvas;

    public void OnClickLadder()
    {
        SoundManager.instance.SFXPlay("stair");
        StartCoroutine(LoadLivingRoom());
    }
    public void OnClickEndingRoomDoor()
    {
        // UICanvas�� ȭ�鿡 ������ �ʰ���
        uiCanvas.sortingOrder = -1;
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
