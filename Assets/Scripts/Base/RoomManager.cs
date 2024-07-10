using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* RoomManager.cs
 * 방을 관리하는 스크립트
 * - 필드 아이템 초기화
 * - 벽면 간의 이동
 * - 방 간의 이동
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
    public Room[] rooms; // For Move
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

    [SerializeField]
    private HashSet<GameObject> trickObjects = new HashSet<GameObject>();

    public List<Trick> tricks = new List<Trick>();

    [SerializeField]
    internal Stack<GameObject> panels = new Stack<GameObject>();

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

    private void OnClickLeftArrow()
    {
        // SoundManager.instance.SFXPlay("arrowButton");

        Room currentRoom = rooms[currentRoomIndex];
        int wallCount = currentRoom.walls.Length;
        currentRoom.walls[currentWallIndex].SetActive(false);
        currentWallIndex = (currentWallIndex + wallCount - 1) % wallCount;
        currentRoom.walls[currentWallIndex].SetActive(true);
    }

    internal void OnClickRightArrow()
    {
        // SoundManager.instance.SFXPlay("arrowButton");

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

    private void SetActiveArrow()
    {
        bool panelsExist = panels.Count > 0;

        leftArrow.SetActive(!panelsExist);
        rightArrow.SetActive(!panelsExist);
        bottomArrow.SetActive(panelsExist);
    }
}
