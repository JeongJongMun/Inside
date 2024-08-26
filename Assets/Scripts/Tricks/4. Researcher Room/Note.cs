using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    private RoomManager roomManager;
    private SoundManager soundManager;
    public GameObject noteZoom;
    private void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
        soundManager = GameManager.instance.soundManager;

        if (TryGetComponent<Button>(out var button)) {
            button.onClick.AddListener(OnClick);
        }
    }
    private void OnClick()
    {
        roomManager.ZoomIn(noteZoom);
        soundManager.Play("turnThePage");
    }
}
