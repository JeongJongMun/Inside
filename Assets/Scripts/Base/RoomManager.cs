using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Define;

public class RoomManager : MonoBehaviour
{
#region Private Variables

#endregion

#region Public Variables

#endregion

#region Private Methods
    private void Awake()
    {
        InitializeItems();

        // leftArrow = GameObject.Find("UICanvas").transform.GetChild(0).gameObject;
        // rightArrow = GameObject.Find("UICanvas").transform.GetChild(1).gameObject;
        // bottomArrow = GameObject.Find("UICanvas").transform.GetChild(2).gameObject;

        // leftArrow.GetComponent<Button>().onClick.AddListener(OnClickLeftArrow);
        // rightArrow.GetComponent<Button>().onClick.AddListener(OnClickRightArrow);
        // bottomArrow.GetComponent<Button>().onClick.AddListener(ZoomOut);
    }
    private void InitializeItems()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items) {
            NewItem addedItem = item.AddComponent<NewItem>();
            addedItem.InitializeItem();
            item.GetComponent<Button>().onClick.AddListener(() => OnClickItem(addedItem));
        }
    }
#endregion

#region Public Methods
    public void OnClickItem(NewItem _item)
    {
        if (NewInventory.instance.AddItem(_item)) {
            Debug.Log($"{_item.name}을 인벤토리에 추가했습니다.");
        }
        else {
            Debug.Log("인벤토리가 가득 찼습니다.");
        }

    }
#endregion

    public GameObject[] wallPanel;

    [SerializeField]
    private int currentWallPanel = 0;

    internal GameObject leftArrow;
    internal GameObject rightArrow;
    internal GameObject bottomArrow;

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

    internal void OnClickRightArrow()
    {
        SoundManager.instance.SFXPlay("arrowButton");
        if (roomName == RoomName.Living)
        {
            wallPanel[currentWallPanel].SetActive(false);
            currentWallPanel = (currentWallPanel + 1) % 2;
            wallPanel[currentWallPanel].SetActive(true);
        }
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
