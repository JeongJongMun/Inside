using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManagerKiller : RoomManager
{
    public void OnClickLadder()
    {
        SceneManager.LoadScene("LivingRoom");
    }
}
