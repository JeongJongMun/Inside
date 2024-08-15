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
        GameManager.instance.soundManager.Play("Butterfly", SoundType.BGM);
    }

    private void OnDisable()
    {
        GameManager.instance.soundManager.Play(roomManager.CurrentRoomName().ToString(), SoundType.BGM);
    }
}
