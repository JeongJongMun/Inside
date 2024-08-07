using UnityEngine;
using UnityEngine.UI;

public class RoomManagerEnding : RoomManager
{
    public GameObject nextArrow;

    public GameObject zoomOutArrow;

    [SerializeField]
    private Canvas uiCanvas;

    public void Start()
    {
        // nextArrow.GetComponent<Button>().onClick.AddListener(OnClickRightArrow);

        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        uiCanvas.sortingOrder = -1;
    }

    public void ZoomInInEnding(GameObject panel)
    {
        // zoomStack.Push(panel);
        panel.SetActive(true);
        zoomOutArrow.SetActive(true);
        nextArrow.GetComponent<Button>().interactable = false;
    }

    public void ZoomOutInEnding()
    {
        SoundManager.instance.SFXPlay("arrowButton");
        // GameObject panel = zoomStack.Pop();
        // panel.SetActive(false);
        zoomOutArrow.SetActive(false);
        nextArrow.GetComponent<Button>().interactable = true;
    }

}
