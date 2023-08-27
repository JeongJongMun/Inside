using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManagerKiller : RoomManager
{
    public Canvas uiCanvas;

    public void OnClickLadder()
    {
        SceneManager.LoadScene("LivingRoom");
    }
    public void OnClickEndingRoomDoor()
    {
        // UICanvas�� ȭ�鿡 ������ �ʰ���
        uiCanvas.sortingOrder = -1;
        SceneManager.LoadScene("EndingRoom");
    }

    public void OnClickMedicine(GameObject medicine)
    {
        medicine.SetActive(false);
    }
}
