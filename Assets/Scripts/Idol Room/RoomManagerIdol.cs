using UnityEngine.SceneManagement;

public class RoomManagerIdol : RoomManager
{
    public void OnClickClosetHole()
    {
        SceneManager.LoadScene("KidRoom");
    }
}
