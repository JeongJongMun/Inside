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
        // UICanvas를 화면에 보이지 않게함
        uiCanvas.sortingOrder = -1;
        SceneManager.LoadScene("EndingRoom");
    }

    public void OnClickMedicine(GameObject medicine)
    {
        medicine.SetActive(false);
    }
}
