using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManagerKid : RoomManager
{
    private List<string> trickNames = new List<string>()
    {
        "DrawerZoom", 
        "Bear",
        "Clock",
        "Curtain", 
        "FamilyPicture", 
        "Safe",
        "WorldMap",
        "Lamp",
        "LampZoom",
        "LegoHole",
        "LegoHole1",
        "LegoHole2",
        "LegoHole3",
        "ConsoleHole",
        "Console",
        "Door",
        "Switch"
    };
    private void Start()
    {
        Initialize(trickNames);
    }

    public void OnClickExitHole()
    {
        SceneManager.LoadScene("IdolRoom");
    }
}
