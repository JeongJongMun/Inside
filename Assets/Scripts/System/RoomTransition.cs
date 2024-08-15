using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{
    private void Start()
    {
        if (TryGetComponent(out Button button)) {
            int direction = gameObject.name == "Next" ? 1 : -1;
            RoomManager roomManager = FindObjectOfType<RoomManager>();
            button.onClick.AddListener(() => roomManager.MoveRoom(direction));
        }
    }
}