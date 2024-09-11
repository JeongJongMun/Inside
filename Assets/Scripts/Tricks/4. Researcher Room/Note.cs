using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    private RoomManager roomManager;
    public GameObject noteZoom;
    private void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();

        if (TryGetComponent<Button>(out var button)) {
            button.onClick.AddListener(OnClick);
        }
    }
    private void OnClick()
    {
        roomManager.ZoomIn(noteZoom);
        Managers.Sound.Play("turnThePage");
    }
}
