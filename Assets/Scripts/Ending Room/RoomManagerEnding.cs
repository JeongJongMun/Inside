using UnityEngine;
using UnityEngine.UI;

public class RoomManagerEnding : RoomManager
{
    [Header("넘어가기 화살표")]
    public GameObject nextArrow;

    [Header("줌아웃 화살표")]
    public GameObject zoomOutArrow;

    public override void Start()
    {
        base.Start();
        nextArrow.GetComponent<Button>().onClick.AddListener(OnClickRightArrow);
    }

    // 엔딩 방에서 ZoomIn 시
    public void ZoomInInEnding(GameObject panel)
    {
        panels.Push(panel);
        panel.SetActive(true);
        zoomOutArrow.SetActive(true);
        nextArrow.GetComponent<Button>().interactable = false;
    }

    // 엔딩 방에서 아래 화살표 클릭 시
    public void ZoomOutInEnding()
    {
        SoundManager.instance.SFXPlay("arrowButton");
        GameObject panel = panels.Pop();
        panel.SetActive(false);
        zoomOutArrow.SetActive(false);
        nextArrow.GetComponent<Button>().interactable = true;
    }

}
