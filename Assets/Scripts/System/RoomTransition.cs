using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{
    private void Start()
    {
        int direction = gameObject.name == "Next" ? 1 : -1;
        if (TryGetComponent(out Button button)) {
            button.onClick.AddListener(() => RoomManager.instance.MoveRoom(direction));
        }
    }
}
