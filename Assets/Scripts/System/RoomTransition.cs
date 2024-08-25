using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{
#region Private Variables
    private const string NEXT = "Next";
    private const string PREV = "Prev";
    private RoomManager roomManager;
    private TrickManager trickManager;
    private SoundManager soundManager;
#endregion

#region Private Methods
    private void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
        trickManager = FindObjectOfType<TrickManager>();
        soundManager = GameManager.instance.soundManager;
        if (TryGetComponent(out Button button)) {
            button.onClick.AddListener(MoveRoom);
        }
    }
    private void MoveRoom()
    {
        int direction = gameObject.name == NEXT ? 1 : -1;
        Define.RoomName currentRoomName = roomManager.CurrentRoomName();

        switch (currentRoomName) {
            case Define.RoomName.Kid:
                soundManager.Play("bedFabric");
                roomManager.MoveRoom(direction);
                break;
            case Define.RoomName.Idol:
                if (direction == 1) {
                    if (trickManager.IsComplete(Define.TrickName.MusicPlate)) {
                        roomManager.MoveRoom(direction);
                        soundManager.Play("doorOpen");
                    }
                    else {
                        soundManager.Play("doorLocked");
                    }
                }
                else {
                    roomManager.MoveRoom(direction);
                    soundManager.Play("bedFabric");
                }
                break;
            case Define.RoomName.Hallway:
                roomManager.MoveRoom(direction);
                if (direction == 1) {
                    soundManager.Play("stair");
                }
                else {
                    soundManager.Play("doorOpen");
                }
                break;
            case Define.RoomName.Living:
                if (direction == 1) {
                    if (trickManager.IsComplete(Define.TrickName.CardReader)) {
                        roomManager.MoveRoom(direction);
                        soundManager.Play("doorOpen");
                    }
                    else {
                        soundManager.Play("doorLocked");
                    }
                }
                else {
                    roomManager.MoveRoom(direction);
                    soundManager.Play("stair");
                }
                break;
            case Define.RoomName.Researcher:
                break;
            case Define.RoomName.CEO:
                break;
            case Define.RoomName.Killer:
                break;
            case Define.RoomName.Ending:
                break;
            case Define.RoomName.Credit:
                break;
        }
    }
#endregion
}