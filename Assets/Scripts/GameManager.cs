using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // ���� ���� GameManager �ν��Ͻ��� �� instance�� ��� �༮�� ����
    // ������ ���� private
    private static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ� �ı� X
        }
        else Destroy(gameObject);
    }

    // GameManager �ν��Ͻ��� �����ϴ� ������Ƽ
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }


    [Header("���ŷ� ����Ʈ")]
    public int mentalPoint = 3;

    [Header("���ŷ� ����Ʈ �̹��� �迭")]
    public GameObject[] mentalImage;

    [Header("����â �г�")]
    public GameObject settingPanel;

    [Header("Ʈ�� ���� ����Ʈ")]
    public Image fadeImage;

    [SerializeField]
    [Header("����Ʈ �ӵ�")]
    [Range(0.01f, 10f)]
    private float fadeTime;

    public void OnClickSettingBtn()
    {
        // ����â�� Ȱ��ȭ ���¶�� ��Ȱ��ȭ
        if (settingPanel.activeSelf) settingPanel.SetActive(false);
        // ����â�� ��Ȱ��ȭ ���¶�� Ȱ��ȭ
        else settingPanel.SetActive(true);
    }

    // ������ Ŭ�� ��
    public void OnClickItem(GameObject _item)
    {
        // �κ��丮�� �߰�
        Inventory.Instance.AcquireItem(_item.GetComponent<Item>());
        // ȭ�鿡 �ִ� ������ ����
        Destroy(_item);
    }

    public void OnClickTestBtn()
    {
        if (SceneManager.GetActiveScene().name == "KidRoom")
            SceneManager.LoadScene("IdolRoom");
        else if (SceneManager.GetActiveScene().name == "IdolRoom")
            SceneManager.LoadScene("KidRoom");
    }

    // Ʈ�� ���� �� ����Ʈ
    public IEnumerator FadeInOut()
    {
        yield return StartCoroutine(Fade(0, 1));

        yield return StartCoroutine(Fade(1, 0));

    }
    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(start, end, percent);
            fadeImage.color = color;
            yield return null;
        }
    }

    // ��Ż ����Ʈ -1
    public void MentalBreak()
    {
        mentalPoint--;
        for (int i = 0; i < 3; i++)
        {
            if (i < mentalPoint)
                mentalImage[i].SetActive(true);
            else 
                mentalImage[i].SetActive(false);
        }

    }

    // ���̹� Ʈ�� ���� ��Ȳ
    private Dictionary<string, bool> isTrickSolved_Kid = new Dictionary<string, bool>()
    {
        { "DrawerZoom",     false},
        { "Bear",           false },
        { "Clock",          false },
        { "Curtain",        false },
        { "FamilyPicture",  false },
        { "Safe",           false },
        { "WorldMap",       false },
        { "Lamp",           false },
        { "LampZoom",       false },
        { "LegoHole",       false },
        { "LegoHole1",      false },
        { "LegoHole2",      false },
        { "LegoHole3",      false },
        { "ConsoleHole",    false },
        { "Console",        false },
        { "Door",           false },
        { "Switch",         false },
    };

    // ���̵��� Ʈ�� ���� ��Ȳ
    private Dictionary<string, bool> isTrickSolved_Idol = new Dictionary<string, bool>()
    {
        { "Closet",     false},
    };
    // Ʈ���� Ǯ�ȴ°� Ȯ��
    public bool IsTrickSolved(string roomName, string trickName)
    {
        switch (roomName) 
        {
            case "Kid":
                return isTrickSolved_Kid[trickName];
            case "Idol":
                return isTrickSolved_Idol[trickName];
            default:
                return false;
        }
    }
    // Ʈ���� Ǯ���ٰ� ����
    public void SetTrickSolved(string roomName, string trickName)
    {
        switch (roomName)
        {
            case "Kid":
                isTrickSolved_Kid[trickName] = true;
                Debug.LogFormat("{0} ���� {1} Ʈ���� �ذ�Ǿ��ٰ� ����", roomName, trickName);
                break;
            case "Idol":
                isTrickSolved_Idol[trickName] = true;
                Debug.LogFormat("{0} ���� {1} Ʈ���� �ذ�Ǿ��ٰ� ����", roomName, trickName);
                break;
            default:
                break;
        }
    }

    // ���̹� ������ ȹ�� ��Ȳ
    private Dictionary<ItemName, bool> isItemAcquired_Kid = new Dictionary<ItemName, bool>()
    {
        {ItemName.Console,     false},
        {ItemName.Cutter,      false},
        {ItemName.KidRoomKey,  false},
        {ItemName.Latch1,      false},
        {ItemName.Lego1,       false},
        {ItemName.Lego2,       false},
        {ItemName.Lego3,       false},
        {ItemName.Password,    false},
    };
    // ���̵��� ������ ȹ�� ��Ȳ
    private Dictionary<ItemName, bool> isItemAcquired_Idol = new Dictionary<ItemName, bool>()
    {
        {ItemName.Broom,     false},
    };

    public bool IsItemAcquired(string roomName, ItemName itemName)
    {
        switch (roomName)
        {
            case "Kid":
                return isItemAcquired_Kid[itemName];
            case "Idol":
                return isItemAcquired_Idol[itemName];
            default: 
                return false;
        }
    }
    public void SetItemAcquired(string roomName, ItemName itemName)
    {
        switch (roomName)
        {
            case "Kid":
                isItemAcquired_Kid[itemName] = true;
                Debug.LogFormat("{0} ���� {1} �������� ȹ���Ͽ��ٰ� ����", roomName, itemName);
                break;
            case "Idol":
                isItemAcquired_Idol[itemName] = true;
                Debug.LogFormat("{0} ���� {1} �������� ȹ���Ͽ��ٰ� ����", roomName, itemName);
                break;
            default:
                break;
        }
    }


}
