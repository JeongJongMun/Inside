using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
/* RoomManager.cs
 * 방을 관리하는 스크립트
 * 필드 아이템 초기화
 * 벽면 이동
 */
[System.Serializable]
public class Room
{
    public GameObject[] walls;
}
public class RoomManager : MonoBehaviour
{
#region Private Variables
    [SerializeField] private int currentRoomIndex = 0;
    [SerializeField] private int currentWallIndex = 0;
#endregion

#region Public Variables
    public Room[] rooms;
    public Transform[] roomPanels; // For Initialize
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject bottomArrow;
#endregion

#region Private Methods
    private void Awake()
    {
        InitializeItems();

        foreach (Transform roomPanel in roomPanels)
        {
            int childCount = roomPanel.childCount;
            for (int i = 0; i < childCount; i++)
            {
                roomPanel.GetChild(i).gameObject.SetActive(false);
            }
        }

        rooms[currentRoomIndex].walls[currentWallIndex].SetActive(true);

        leftArrow.GetComponent<Button>().onClick.AddListener(OnClickLeftArrow);
        rightArrow.GetComponent<Button>().onClick.AddListener(OnClickRightArrow);
        bottomArrow.GetComponent<Button>().onClick.AddListener(ZoomOut);
    }
    private void InitializeItems()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items) {
            NewItem addedItem = item.AddComponent<NewItem>();
            addedItem.InitializeItem();
            item.GetComponent<Button>().onClick.AddListener(() => NewInventory.instance.AddItem(addedItem));
        }
    }
#endregion

#region Public Methods
#endregion

    public GameObject[] wallPanel;
    [SerializeField]
    private HashSet<GameObject> trickObjects = new HashSet<GameObject>();

    public List<Trick> tricks = new List<Trick>();

    [SerializeField]
    internal Stack<GameObject> panels = new Stack<GameObject>();

    [SerializeField]
    private RoomName roomName;

    public virtual void Start()
    {
        roomName = Item.GetEnumFromName<RoomName>(this.name.Substring(11));
        Initialize("Trick");
    }

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

    internal void Initialize(string tag)
    {
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

    private void OnClickLeftArrow()
    {
        // SoundManager.instance.SFXPlay("arrowButton");
        // if (roomName == RoomName.Living)
        // {
        //     wallPanel[currentWallIndex].SetActive(false);
        //     currentWallIndex = (currentWallIndex + 1) % 2;
        //     wallPanel[currentWallIndex].SetActive(true);
        // }
        // else
        // {
        //     wallPanel[currentWallIndex].SetActive(false);
        //     currentWallIndex = (currentWallIndex + 3) % 4;
        //     wallPanel[currentWallIndex].SetActive(true);
        // }

        Room currentRoom = rooms[currentRoomIndex];
        int wallCount = currentRoom.walls.Length;
        currentRoom.walls[currentWallIndex].SetActive(false);
        currentWallIndex = (currentWallIndex + wallCount - 1) % wallCount;
        currentRoom.walls[currentWallIndex].SetActive(true);
    }

    internal void OnClickRightArrow()
    {
        // SoundManager.instance.SFXPlay("arrowButton");
        // if (roomName == RoomName.Living)
        // {
        //     wallPanel[currentWallIndex].SetActive(false);
        //     currentWallIndex = (currentWallIndex + 1) % 2;
        //     wallPanel[currentWallIndex].SetActive(true);
        // }
        // else if (roomName == RoomName.Ending)
        // {
        //     if (currentWallIndex == 2)
        //     {
        //         SceneManager.LoadScene("Credit");
        //         return;
        //     }
        //     wallPanel[currentWallIndex].SetActive(false);
        //     currentWallIndex++;
        //     wallPanel[currentWallIndex].SetActive(true);
        // }
        // else
        // {
        //     wallPanel[currentWallIndex].SetActive(false);
        //     currentWallIndex = (currentWallIndex + 1) % 4;
        //     wallPanel[currentWallIndex].SetActive(true);
        // }
        Room currentRoom = rooms[currentRoomIndex];
        int wallCount = currentRoom.walls.Length;
        currentRoom.walls[currentWallIndex].SetActive(false);
        currentWallIndex = (currentWallIndex + 1) % wallCount;
        currentRoom.walls[currentWallIndex].SetActive(true);
    }
    public void ZoomIn(GameObject panel)
    {
        panels.Push(panel);
        panel.SetActive(true);
        SetActiveArrow();
    }
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

    public void OnClickTrick(GameObject obj)
    {
        NotifyTricks(obj);
    }

    private void SetActiveArrow()
    {
        bool panelsExist = panels.Count > 0;

        leftArrow.SetActive(!panelsExist);
        rightArrow.SetActive(!panelsExist);
        bottomArrow.SetActive(panelsExist);
    }


    internal void AddTrick(Trick trick)
    {
        if (tricks.Contains(trick))
        {
            Debug.Log("트릭이 이미 리스트에 존재합니다.");
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
        yield return new WaitForSeconds(0.3f);

        if (tricks.Contains(trick))
        {
            tricks.Remove(trick);
        }
        else
        {
            Debug.Log("트릭이 리스트에 존재하지 않습니다.");
        }

        yield return null;
    }
}
