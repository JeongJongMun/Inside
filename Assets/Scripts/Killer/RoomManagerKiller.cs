using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManagerKiller : RoomManager
{
    [SerializeField]
    private Canvas uiCanvas;

    public override void Start()
    {
        base.Start();
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
    }
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
