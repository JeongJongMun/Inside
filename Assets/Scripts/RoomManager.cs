using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [Header("1~4�� ����")]
    public GameObject[] wallPanel;

    [SerializeField]
    [Header("���� ���� (+1)")]
    private int currentWallPanel = 0;

    [Header("��/��/�� ȭ��ǥ")]
    internal GameObject leftArrow;
    internal GameObject rightArrow;
    internal GameObject bottomArrow;

    [Header("���� ���� Ʈ����")]
    public List<Trick> tricks = new List<Trick>();

    [SerializeField]
    [Header("���� ���� ���̴� �г� ���� ex) ��, �����̵� Ʈ��")]
    private Stack<GameObject> panels = new Stack<GameObject>();

    private void Awake()
    {
        leftArrow = GameObject.Find("UICanvas").transform.GetChild(0).gameObject;
        rightArrow = GameObject.Find("UICanvas").transform.GetChild(1).gameObject;
        bottomArrow = GameObject.Find("UICanvas").transform.GetChild(2).gameObject;

        leftArrow.GetComponent<Button>().onClick.AddListener(OnClickLeftArrow);
        rightArrow.GetComponent<Button>().onClick.AddListener(OnClickRightArrow);
        bottomArrow.GetComponent<Button>().onClick.AddListener(ZoomOut);
    }
    internal void Initialize(List<string> trickNames)
    {
        // �θ� ������Ʈ ã��
        GameObject canvas = GameObject.Find("Canvas");

        foreach (var trick in trickNames)
        {
            GameObject childObject = FindDeepChild(canvas, trick);

            if (childObject != null)
            {
                Trick[] tricks = childObject.GetComponents<Trick>();
                foreach (Trick t in tricks)
                {
                    AddTrick(t);
                    Debug.LogFormat("Found Trick: {0}", childObject.name);
                }
            }
            else
            {
                Debug.LogFormat("{0} Trick Not Founded", childObject.name);
            }
        }
    }

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
            trick.TrySolve(obj);
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
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            bottomArrow.SetActive(true);
        }
        else
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            bottomArrow.SetActive(false);
        }   
    }
    // Ʈ���� ��������� ã��
    internal GameObject FindDeepChild(GameObject parent, string name)
    {
        Transform parentTransform = parent.transform;

        if (parent.name == name)
        {
            return parent;
        }

        foreach (Transform child in parentTransform)
        {
            GameObject result = FindDeepChild(child.gameObject, name);
            if (result != null)
                return result;
        }

        return null;
    }

    internal void AddTrick(Trick trick)
    {
        if (tricks.Contains(trick))
        {
            Debug.Log("�̹� �ش� Ʈ���� ����Ʈ�� �����ϰ� ����.");
        }
        else
        {
            tricks.Add(trick);
        }
    }
    internal void RemoveTrick(Trick trick)
    {
        if (tricks.Contains(trick))
        {
            tricks.Remove(trick);
        }
        else
        {
            Debug.Log("�ش� Ʈ���� ����Ʈ�� �������� �ʾƼ� �������� ����.");
        }
    }
}
