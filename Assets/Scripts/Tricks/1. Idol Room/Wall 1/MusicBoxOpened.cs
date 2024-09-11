using UnityEngine;

public class MusicBoxOpened : MonoBehaviour
{
    private RoomManager roomManager;
    private void Awake()
    {
        roomManager = FindObjectOfType<RoomManager>();
    }
    private void OnEnable()
    {
        Managers.Sound.Play("Butterfly", SoundType.BGM);
    }

    private void OnDisable()
    {
        Managers.Sound.Play(roomManager.CurrentRoomName().ToString(), SoundType.BGM);
    }
}
