using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // 게임 내에 GameManager 인스턴스는 이 instance에 담긴 녀석만 존재
    // 보안을 위해 private
    private static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에 파괴 X
        }
        else Destroy(gameObject);
    }

    // GameManager 인스턴스에 접근하는 프로퍼티
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


    [Header("정신력 포인트")]
    public int mentalPoint = 3;

    [Header("정신력 포인트 이미지 배열")]
    public GameObject[] mentalImage;

    [Header("설정창 패널")]
    public GameObject settingPanel;

    [Header("트릭 성공 이펙트")]
    public Image fadeImage;

    [SerializeField]
    [Header("이펙트 속도")]
    [Range(0.01f, 10f)]
    private float fadeTime;

    public void OnClickSettingBtn()
    {
        // 설정창이 활성화 상태라면 비활성화
        if (settingPanel.activeSelf) settingPanel.SetActive(false);
        // 설정창이 비활성화 상태라면 활성화
        else settingPanel.SetActive(true);
    }

    // 아이템 클릭 시
    public void OnClickItem(GameObject _item)
    {
        // 인벤토리에 추가
        Inventory.Instance.AcquireItem(_item.GetComponent<Item>());
        // 화면에 있는 아이템 삭제
        Destroy(_item);
    }

    public void OnClickTestBtn()
    {
        if (SceneManager.GetActiveScene().name == "KidRoom")
            SceneManager.LoadScene("IdolRoom");
        else if (SceneManager.GetActiveScene().name == "IdolRoom")
            SceneManager.LoadScene("KidRoom");
    }

    // 트릭 성공 시 이펙트
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

    // 멘탈 포인트 -1
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

    // 아이방 트릭 진행 상황
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

    // 아이돌방 트릭 진행 상황
    private Dictionary<string, bool> isTrickSolved_Idol = new Dictionary<string, bool>()
    {
        { "Closet",     false},
    };
    // 트릭이 풀렸는가 확인
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
    // 트릭을 풀었다고 설정
    public void SetTrickSolved(string roomName, string trickName)
    {
        switch (roomName)
        {
            case "Kid":
                isTrickSolved_Kid[trickName] = true;
                Debug.LogFormat("{0} 방의 {1} 트릭이 해결되었다고 저장", roomName, trickName);
                break;
            case "Idol":
                isTrickSolved_Idol[trickName] = true;
                Debug.LogFormat("{0} 방의 {1} 트릭이 해결되었다고 저장", roomName, trickName);
                break;
            default:
                break;
        }
    }

    // 아이방 아이템 획득 상황
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
    // 아이돌방 아이템 획득 상황
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
                Debug.LogFormat("{0} 방의 {1} 아이템을 획득하였다고 저장", roomName, itemName);
                break;
            case "Idol":
                isItemAcquired_Idol[itemName] = true;
                Debug.LogFormat("{0} 방의 {1} 아이템을 획득하였다고 저장", roomName, itemName);
                break;
            default:
                break;
        }
    }


}
