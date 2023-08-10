using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManagerIdol : RoomManager
{
    public void OnClickClosetHole()
    {
        SceneManager.LoadScene("KidRoom");
    }
    public void OnClickDrawer(GameObject desk)
    {
        desk.SetActive(false);
    }
}