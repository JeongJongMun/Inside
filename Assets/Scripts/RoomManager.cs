using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] wallPanel;
    private int currentWallPanel = 0;

    public GameObject[] wallPanelZoom;

    public GameObject[] arrows; // 순서대로 왼쪽, 오른쪽, 아래쪽 화살표

    public List<Trick> tricks = new List<Trick>();



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
    // 확대
    public void OnZoom()
    {
        SetActiveArrow();
        wallPanel[currentWallPanel].SetActive(false);
        if (currentWallPanel == 3) 
            wallPanelZoom[2].SetActive(true);
        else wallPanelZoom[currentWallPanel].SetActive(true);
    }
    // 아래쪽 화살표 클릭 시
    public void OnClickBottomArrow()
    {
        SetActiveArrow();
        wallPanel[currentWallPanel].SetActive(true);
        if (currentWallPanel == 3)
            wallPanelZoom[2].SetActive(false);
        else wallPanelZoom[currentWallPanel].SetActive(false);
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
    public void SetActiveArrow()
    {
        foreach (GameObject arrow in arrows)
        {
            if (arrow.activeSelf) arrow.SetActive(false);
            else arrow.SetActive(true);
        }
    }
}
