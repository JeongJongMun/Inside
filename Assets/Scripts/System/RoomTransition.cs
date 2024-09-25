using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{
#region Private Variables
    private const string NEXT = "Next";
    private const string PREV = "Prev";
    private RoomManager roomManager;
#endregion

#region Private Methods
    private void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
        if (TryGetComponent(out Button button)) {
            button.onClick.AddListener(MoveRoom);
        }
    }
    private void MoveRoom()
    {
        int direction = gameObject.name == NEXT ? 1 : -1;
        Define.ERoomType currentERoomType = roomManager.CurrentRoomName();

        switch (currentERoomType) {
            case Define.ERoomType.Kid:
                Managers.Sound.Play("bedFabric");
                roomManager.MoveRoom(direction);
                break;
            case Define.ERoomType.Idol:
                if (direction == 1) {
                    if (Managers.Trick.IsComplete(Define.ETrickType.MusicPlate)) {
                        roomManager.MoveRoom(direction);
                        Managers.Sound.Play("doorOpen");
                    }
                    else {
                        Managers.Sound.Play("doorLocked");
                    }
                }
                else {
                    roomManager.MoveRoom(direction);
                    Managers.Sound.Play("bedFabric");
                }
                break;
            case Define.ERoomType.Hallway:
                roomManager.MoveRoom(direction);
                if (direction == 1) {
                    Managers.Sound.Play("stair");
                }
                else {
                    Managers.Sound.Play("doorOpen");
                }
                break;
            case Define.ERoomType.Living:
                if (direction == 1) {
                    if (Managers.Trick.IsComplete(Define.ETrickType.CardReader)) {
                        roomManager.MoveRoom(direction);
                        Managers.Sound.Play("doorOpen");
                    }
                    else {
                        Managers.Sound.Play("doorLocked");
                    }
                }
                else {
                    roomManager.MoveRoom(direction);
                    Managers.Sound.Play("stair");
                }
                break;
            case Define.ERoomType.Researcher:
                break;
            case Define.ERoomType.CEO:
                break;
            case Define.ERoomType.Killer:
                break;
            case Define.ERoomType.Ending:
                break;
            case Define.ERoomType.Credit:
                break;
        }
    }
#endregion
}