using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    /// <summary>
    /// �� �� �Ŵ��� ����
    /// 1. �� ��ȯ �� Ȯ��
    /// 2. �� ���� �� ��� ȭ��ǥ ����
    /// 3. ���� ���� ��� Ʈ�� ���� (Ʈ�� ������Ʈ�� tag�� Trick���� �����Ͽ��� ��)
    /// 4. ��� Ʈ�� �����ϸ鼭 Database Manager�� Ʈ�� �ڵ� �߰�
    /// 5. Ʈ�� Ŭ�� �� ��� Ʈ������ �˸� (������ ����)
    /// </summary>
    [Header("1~4�� ����")]
    public GameObject[] wallPanel;

    [SerializeField]
    [Header("���� ���� (+1)")]
    private int currentWallPanel = 0;

    [Header("��/��/�� ȭ��ǥ")]
    internal GameObject leftArrow;
    internal GameObject rightArrow;
    internal GameObject bottomArrow;

    [SerializeField]
    [Header("���� ���� Ʈ�� ������Ʈ��")]
    private HashSet<GameObject> trickObjects = new HashSet<GameObject>();

    [Header("���� ���� Ʈ����")]
    public List<Trick> tricks = new List<Trick>();

    [SerializeField]
    [Header("���� ���� ���̴� �г� ���� ex) ��, �����̵� Ʈ��")]
    private Stack<GameObject> panels = new Stack<GameObject>();

    [SerializeField]
    private string roomName;

    private void Awake()
    {
        leftArrow = GameObject.Find("UICanvas").transform.GetChild(0).gameObject;
        rightArrow = GameObject.Find("UICanvas").transform.GetChild(1).gameObject;
        bottomArrow = GameObject.Find("UICanvas").transform.GetChild(2).gameObject;

        leftArrow.GetComponent<Button>().onClick.AddListener(OnClickLeftArrow);
        rightArrow.GetComponent<Button>().onClick.AddListener(OnClickRightArrow);
        bottomArrow.GetComponent<Button>().onClick.AddListener(ZoomOut);
    }
    private void Start()
    {
        roomName = this.name.Substring(11);
        Initialize("Trick");
    }

    // Ʈ���� ��������� ã��
    internal void FindDeepChild(GameObject parent, string _tag)
    {
        Transform parentTransform = parent.transform;

        if (parent.tag == _tag && !trickObjects.Contains(parent))
        {
            trickObjects.Add(parent);
        }

        foreach (Transform child in parentTransform)
        {
            FindDeepChild(child.gameObject, _tag);
        }
    }

    // �� ���� �� �� ���� ��� Ʈ���� tag�� ã��
    internal void Initialize(string tag)
    {
        // �θ� ������Ʈ ã��
        GameObject canvas = GameObject.Find("Canvas");

        FindDeepChild(canvas, tag);

        if (trickObjects != null)
        {
            foreach (GameObject obj in trickObjects)
            {
                // DatabaseManger�� Ʈ�� �߰� 
                if (!DatabaseManager.Instance.IsTrickExist(roomName, obj.name))
                {
                    DatabaseManager.Instance.SetTrickStatus(roomName, obj.name, false);

                }
                AddTrick(obj.GetComponent<Trick>());
                Debug.LogFormat("Found Trick: {0}", obj.name);
            }
        }
        else
        {
            Debug.LogFormat("Trick Not Founded");
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
