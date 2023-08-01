using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] wallPanel;
    private int currentWallPanel = 0;

    public GameObject[] wallPanelZoom;

    public GameObject[] arrows; // ������� ����, ������, �Ʒ��� ȭ��ǥ

    public List<Trick> tricks = new List<Trick>();



    // ���� ȭ��ǥ Ŭ�� ��
    public void OnClickLeftArrow()
    {
        wallPanel[currentWallPanel].SetActive(false);
        currentWallPanel = (currentWallPanel + 3) % 4;
        wallPanel[currentWallPanel].SetActive(true);

    }

    // ������ ȭ��ǥ Ŭ�� ��
    public void OnClickRightArrow()
    {
        wallPanel[currentWallPanel].SetActive(false);
        currentWallPanel = (currentWallPanel + 1) % 4;
        wallPanel[currentWallPanel].SetActive(true);
    }
    // Ȯ��
    public void OnZoom()
    {
        SetActiveArrow();
        wallPanel[currentWallPanel].SetActive(false);
        if (currentWallPanel == 3) 
            wallPanelZoom[2].SetActive(true);
        else wallPanelZoom[currentWallPanel].SetActive(true);
    }
    // �Ʒ��� ȭ��ǥ Ŭ�� ��
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

    // Ʈ�� Ŭ�� �� Ʈ���鿡�� �˸�
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
