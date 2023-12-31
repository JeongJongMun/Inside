using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Define;

public class RoomManager : MonoBehaviour
{
    /// <summary>
    /// 각 방 매니저 역할
    /// 1. 벽 전환 및 확대
    /// 2. 방 시작 시 모든 화살표 참조
    /// 3. 현재 방의 모든 트릭 참조 (트릭 오브젝트에 tag를 Trick으로 설정하여야 함)
    /// 4. 모든 트릭 참조하면서 Database Manager에 트릭 자동 추가
    /// 5. 트릭 클릭 시 모든 트릭에게 알림 (옵저버 패턴)
    /// </summary>
    [Header("1~4번 벽면")]
    public GameObject[] wallPanel;

    [SerializeField]
    [Header("현재 벽면 (+1)")]
    private int currentWallPanel = 0;

    [Header("좌/우/하 화살표")]
    internal GameObject leftArrow;
    internal GameObject rightArrow;
    internal GameObject bottomArrow;

    [SerializeField]
    [Header("현재 방의 트릭 오브젝트들")]
    private HashSet<GameObject> trickObjects = new HashSet<GameObject>();

    [Header("현재 방의 트릭들")]
    public List<Trick> tricks = new List<Trick>();

    [SerializeField]
    [Header("벽면 위에 쌓이는 패널 스택 ex) 줌, 슬라이딩 트릭")]
    internal Stack<GameObject> panels = new Stack<GameObject>();

    [SerializeField]
    [Header("방 이름")]
    private RoomName roomName;

    private void Awake()
    {
        leftArrow = GameObject.Find("UICanvas").transform.GetChild(0).gameObject;
        rightArrow = GameObject.Find("UICanvas").transform.GetChild(1).gameObject;
        bottomArrow = GameObject.Find("UICanvas").transform.GetChild(2).gameObject;

        leftArrow.GetComponent<Button>().onClick.AddListener(OnClickLeftArrow);
        rightArrow.GetComponent<Button>().onClick.AddListener(OnClickRightArrow);
        bottomArrow.GetComponent<Button>().onClick.AddListener(ZoomOut);
    }
    public virtual void Start()
    {
        roomName = Item.GetEnumFromName<RoomName>(this.name.Substring(11));
        Initialize("Trick");
    }

    // 트릭을 재귀적으로 찾기
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

    // 방 시작 시 방 안의 모든 트릭을 tag로 찾음
    internal void Initialize(string tag)
    {
        // 부모 오브젝트 찾기
        GameObject canvas = GameObject.Find("Canvas");

        FindDeepChild(canvas, tag);

        if (trickObjects != null)
        {
            foreach (GameObject obj in trickObjects)
            {
                AddTrick(obj.GetComponent<Trick>());
            }
        }
        else
        {
            Debug.LogFormat("Trick Not Founded");
        }
    }

    // 왼쪽 화살표 클릭 시
    private void OnClickLeftArrow()
    {
        SoundManager.instance.SFXPlay("arrowButton");
        if (roomName == RoomName.Living)
        {
            wallPanel[currentWallPanel].SetActive(false);
            currentWallPanel = (currentWallPanel + 1) % 2;
            wallPanel[currentWallPanel].SetActive(true);
        }
        else
        {
            wallPanel[currentWallPanel].SetActive(false);
            currentWallPanel = (currentWallPanel + 3) % 4;
            wallPanel[currentWallPanel].SetActive(true);
        }
    }

    // 오른쪽 화살표 클릭 시
    internal void OnClickRightArrow()
    {
        SoundManager.instance.SFXPlay("arrowButton");
        if (roomName == RoomName.Living)
        {
            wallPanel[currentWallPanel].SetActive(false);
            currentWallPanel = (currentWallPanel + 1) % 2;
            wallPanel[currentWallPanel].SetActive(true);
        }
        // 엔딩방 예외 처리
        else if (roomName == RoomName.Ending)
        {
            if (currentWallPanel == 2)
            {
                SceneManager.LoadScene("Credit");
                return;
            }
            wallPanel[currentWallPanel].SetActive(false);
            currentWallPanel++;
            wallPanel[currentWallPanel].SetActive(true);
        }
        else
        {
            wallPanel[currentWallPanel].SetActive(false);
            currentWallPanel = (currentWallPanel + 1) % 4;
            wallPanel[currentWallPanel].SetActive(true);
        }
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
        SoundManager.instance.SFXPlay("arrowButton");
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
    private void SetActiveArrow()
    {
        // 패널 스택에 1개라도 있다면 좌우 화살표는 없고 아래쪽 화살표만 있어야 함
        bool panelsExist = panels.Count > 0;

        leftArrow.SetActive(!panelsExist);
        rightArrow.SetActive(!panelsExist);
        bottomArrow.SetActive(panelsExist);
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
        StartCoroutine(DoRemoveTrick(trick));
    }
    private IEnumerator DoRemoveTrick(Trick trick)
    {
        // NotifyTricks() 함수가 실행되고 있는 와중에 삭제가 되어버려 에러가 발생할 수 있음. -> 0.3초 뒤에 삭제
        yield return new WaitForSeconds(0.3f);

        if (tricks.Contains(trick))
        {
            tricks.Remove(trick);
        }
        else
        {
            Debug.Log("해당 트릭이 리스트에 존재하지 않아서 제거하지 못함.");
        }

        yield return null;
    }
}
