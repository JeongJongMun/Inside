using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RoomManagerIdol : RoomManager
{
    
    private List<string> trickNames = new List<string>()
    {
        "Closet",

    };
    private void Start()
    {
        Initialize(trickNames);
    }
    public void OnClickClosetHole()
    {
        SceneManager.LoadScene("KidRoom");
    }
}
