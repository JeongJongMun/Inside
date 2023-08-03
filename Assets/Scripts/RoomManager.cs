using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // 4�� �� �г�
    public GameObject[] wallPanel;
    private int currentWallPanel = 0;

    // ������� ����, ������, �Ʒ��� ȭ��ǥ
    public GameObject[] arrows; 

    public List<Trick> tricks = new List<Trick>();

    // ��&�����̵� ���� �г� ����
    private Stack<GameObject> panels = new Stack<GameObject>();



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
    // ������Ʈ Ŭ�� �� Ȯ�� OR �����̵� ���� �г� �ѱ�
    public void ZoomIn(GameObject panel)
    {
        panels.Push(panel);
        panel.SetActive(true);
        SetActiveArrow();
    }
    // �Ʒ��� ȭ��ǥ Ŭ�� �� ��� OR �����̵� �г� ����
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

    // Ʈ�� Ŭ�� �� Ʈ���鿡�� �˸�
    public void OnClickTrick(GameObject obj)
    {
        NotifyTricks(obj);
    }

    // �¿� ȭ��ǥ�� �Ʒ� ȭ��ǥ Ȱ��ȭ ����
    public void SetActiveArrow()
    {
        // �г� ���ÿ� 1���� �ִٸ� (�¿� ȭ��ǥ�� ���� �Ʒ��� ȭ��ǥ�� �־�� ��)
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
