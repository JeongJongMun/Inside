using UnityEngine;
using UnityEngine.UI;

public class RoomManagerEnding : RoomManager
{
    [Header("�Ѿ�� ȭ��ǥ")]
    public GameObject nextArrow;

    [Header("�ܾƿ� ȭ��ǥ")]
    public GameObject zoomOutArrow;

    [SerializeField]
    private Canvas uiCanvas;

    public override void Start()
    {
        base.Start();
        nextArrow.GetComponent<Button>().onClick.AddListener(OnClickRightArrow);

        // UI Canvas�� Sorting Order�� -1�� �����Ͽ�, ���� �濡�� UI�� ������ �ʵ��� ��
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        uiCanvas.sortingOrder = -1;
    }

    // ���� �濡�� ZoomIn ��
    public void ZoomInInEnding(GameObject panel)
    {
        panels.Push(panel);
        panel.SetActive(true);
        zoomOutArrow.SetActive(true);
        nextArrow.GetComponent<Button>().interactable = false;
    }

    // ���� �濡�� �Ʒ� ȭ��ǥ Ŭ�� ��
    public void ZoomOutInEnding()
    {
        SoundManager.instance.SFXPlay("arrowButton");
        GameObject panel = panels.Pop();
        panel.SetActive(false);
        zoomOutArrow.SetActive(false);
        nextArrow.GetComponent<Button>().interactable = true;
    }

}
