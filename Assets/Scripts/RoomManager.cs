using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // 4면 벽 패널
    public GameObject[] wallPanel;
    private int currentWallPanel = 0;

    // 순서대로 왼쪽, 오른쪽, 아래쪽 화살표
    public GameObject[] arrows; 

    public List<Trick> tricks = new List<Trick>();

    // 줌&슬라이딩 게임 패널 스택
    private Stack<GameObject> panels = new Stack<GameObject>();



    // 왼쪽 화살표 클릭 시
    public void OnClickLeftArrow()
    {
        wallPanel[currentWallPanel].SetActive(false);
        currentWallPanel = (currentWallPanel + 3) % 4;
        wallPanel[currentWallPanel].SetActive(true);
    }

    // 오른쪽 화살표 클릭 시
    public void OnClickRightArrow()
    {
        wallPanel[currentWallPanel].SetActive(false);
        currentWallPanel = (currentWallPanel + 1) % 4;
        wallPanel[currentWallPanel].SetActive(true);
    }
    // 오브젝트 클릭 시 확대 OR 슬라이딩 퍼즐 패널 켜기
    public void ZoomIn(GameObject panel)
    {
        panels.Push(panel);
        panel.SetActive(true);
        SetActiveArrow();
    }
    // 아래쪽 화살표 클릭 시 축소 OR 슬라이딩 패널 끄기
    public void ZoomOut()
    {
        GameObject panel = panels.Pop();
        panel.SetActive(false);
        SetActiveArrow();
    }

    void NotifyTricks(GameObject obj)
    {
        foreach (Trick trick in tricks)
        {
            trick.SolveOrNotSolve(obj);
        }
    }

    // 트릭 클릭 시 트릭들에게 알림
    public void OnClickTrick(GameObject obj)
    {
        NotifyTricks(obj);
    }

    // 좌우 화살표와 아래 화살표 활성화 관리
    public void SetActiveArrow()
    {
        // 패널 스택에 1개라도 있다면 (좌우 화살표는 없고 아래쪽 화살표만 있어야 함)
        if (panels.Count > 0)
        {
            arrows[0].SetActive(false);
            arrows[1].SetActive(false);
            arrows[2].SetActive(true);
        }
        else
        {
            arrows[0].SetActive(true);
            arrows[1].SetActive(true);
            arrows[2].SetActive(false);
        }
    }
}
