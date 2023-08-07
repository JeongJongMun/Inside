using UnityEngine.SceneManagement;

public class RoomManagerKid : RoomManager
{
    public void OnClickExitHole()
    {
        SceneManager.LoadScene("IdolRoom");
    }
}
