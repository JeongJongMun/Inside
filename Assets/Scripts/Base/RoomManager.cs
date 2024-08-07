using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* RoomManager.cs
 * 방을 관리하는 스크립트
 * - 필드 아이템 초기화
 * - 벽면 간의 이동
 * - 방 간의 이동
 */
[Serializable]
public class Room
{
    public GameObject[] walls;
}
public class RoomManager : MonoBehaviour
{
#region Private Variables
    [SerializeField] private int currentRoomIndex = 0;
    [SerializeField] private int currentWallIndex = 0;
    [SerializeField] private Room[] rooms;
    [SerializeField] private Transform[] subwalls;
    [SerializeField] private int roomCount = 0;
    private Stack<GameObject> zoomStack = new Stack<GameObject>();
    private List<string> roomNames = new List<string>();
    private GameManager gameManager;
#endregion

#region Public Variables
    public static RoomManager instance;
    public string currentRoomName => roomNames[currentRoomIndex];

    public Action OnRoomChanged;
    public Transform roomHolder;
    public GameObject leftArrow, rightArrow, bottomArrow;
#endregion

#region Private Methods
    private void Awake()
    {
        instance = this;
        OnRoomChanged += () => gameManager.soundManager.Play($"{currentRoomName}", SoundType.BGM);
        InitializeItems();
        InitializeRooms();

        // Initialize roomNames
        for (int i = 0; i < roomCount; i++) {
            roomNames.Add(roomHolder.GetChild(i).GetComponent<RoomInfo>().roomName);
        }

        leftArrow.GetComponent<Button>().onClick.AddListener(() => MoveWall(-1));
        rightArrow.GetComponent<Button>().onClick.AddListener(() => MoveWall(1));
        bottomArrow.GetComponent<Button>().onClick.AddListener(ZoomOut);

        gameManager = GameManager.Instance;
    }
    private void Start()
    {
        for (int i = 0; i < roomCount; i++) {
            for (int j = 0; j < rooms[i].walls.Length; j++) {
                rooms[i].walls[j].SetActive(false);
            }
            if (subwalls[i] == null) continue;
            for (int j = 0; j < subwalls[i].childCount; j++) {
                subwalls[i].GetChild(j).gameObject.SetActive(false);
            }
        }
        rooms[currentRoomIndex].walls[currentWallIndex].SetActive(true);
    }
    private void InitializeItems()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items) {
            NewItem addedItem = item.AddComponent<NewItem>();
            addedItem.InitializeItem();
            item.GetComponent<Button>().onClick.AddListener(() => {
                NewInventory.instance.AddItem(addedItem);
                gameManager.soundManager.Play("Item", SoundType.EFFECT);

            });
        }
    }
    private void InitializeRooms()
    {
        roomCount = roomHolder.childCount;

        rooms = new Room[roomCount];
        subwalls = new Transform[roomCount];
        for (int i = 0; i < roomCount; i++) {
            Transform room = roomHolder.GetChild(i);

            Transform wallHolder = room.Find("Wall Holder");
            if (wallHolder == null) {
                rooms[i] = new Room {
                    walls = new GameObject[0]
                };
                continue;
            }
            int wallCount = wallHolder.childCount;
            rooms[i] = new Room {
                walls = new GameObject[wallCount]
            };
            for (int j = 0; j < wallCount; j++){
                rooms[i].walls[j] = wallHolder.GetChild(j).gameObject;
            }

            Transform subwallHolder = room.Find("Subwall Holder");
            if (subwallHolder == null) {
                subwalls[i] = null;
                continue;
            }
            subwalls[i] = subwallHolder;
        }
    }
    private void MoveWall(int _direction)
    {
        gameManager.soundManager.Play("ArrowButton", SoundType.EFFECT);
        GameObject[] walls = rooms[currentRoomIndex].walls;
        int wallCount = walls.Length;

        walls[currentWallIndex].SetActive(false);
        currentWallIndex = (currentWallIndex + wallCount + _direction) % wallCount;
        walls[currentWallIndex].SetActive(true);
    }
    private void SetActiveArrow()
    {
        bool panelsExist = zoomStack.Count > 0;

        leftArrow.SetActive(!panelsExist);
        rightArrow.SetActive(!panelsExist);
        bottomArrow.SetActive(panelsExist);
    }
#endregion

#region Public Methods
    public void ZoomIn(GameObject panel)
    {
        zoomStack.Push(panel);
        panel.SetActive(true);
        SetActiveArrow();
    }
    public void ZoomOut()
    {
        GameObject panel = zoomStack.Pop();
        panel.SetActive(false);
        SetActiveArrow();
    }
#endregion

    [SerializeField]
    private HashSet<GameObject> trickObjects = new HashSet<GameObject>();

    public List<Trick> tricks = new List<Trick>();
}