using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [Header("1~4번 벽면")]
    public GameObject[] wallPanel;

    [SerializeField]
    [Header("현재 벽면 (+1)")]
    private int currentWallPanel = 0;

    [Header("좌/우/하 화살표")]
    internal GameObject leftArrow;
    internal GameObject rightArrow;
    internal GameObject bottomArrow;

    [Header("현재 방의 트릭들")]
    public List<Trick> tricks = new List<Trick>();

    [SerializeField]
    [Header("벽면 위에 쌓이는 패널 스택 ex) 줌, 슬라이딩 트릭")]
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
        // 부모 오브젝트 찾기
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
            trick.TrySolve(obj);
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
    // 트릭을 재귀적으로 찾기
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
            Debug.Log("이미 해당 트릭이 리스트에 존재하고 있음.");
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
            Debug.Log("해당 트릭이 리스트에 존재하지 않아서 제거하지 못함.");
        }
    }
}
