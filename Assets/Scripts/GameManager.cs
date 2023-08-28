using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // 게임 내에 GameManager 인스턴스는 이 instance에 담긴 녀석만 존재
    // 보안을 위해 private
    private static GameManager instance = null;

    //For Audio volume Slider
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Slider audioSlider;

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

    // for Audio Slider
    private void Start()
    {
        // SetMusicVolume();
    }

    public void SetMusicVolume(Slider slider)
    {
        float volume = slider.value;
        masterMixer.SetFloat("BGM", Mathf.Log10(volume)*20);
    }


    [Header("정신력 포인트")]
    public int mentalPoint = 3;

    [Header("정신력 포인트 이미지 배열")]
    public GameObject[] mentalImage;

    [Header("설정창 패널")]
    public GameObject settingPanel;

    [Header("게임오버 패널")]
    public GameObject gameoverPanel;

    [Header("트릭 성공 이펙트")]
    public Image fadeImage;

    [SerializeField]
    [Header("이펙트 속도")]
    [Range(0.01f, 10f)]
    private float fadeTime;

    [Header("UI Canvas")]
    public Canvas uiCanvas;


    // 설정 버튼 -> 설정 패널 ON
    public void SettingPanelOnOff()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }
    // 설정 패널 - 게임종료 & 게임오버 패널 - 메인으로
    public void OnClickExitBtn(GameObject panel)
    {
        panel.SetActive(false);
        SoundManager.instance.SFXPlay("buttonSound");
        UICanvasSetActive();
        StartCoroutine(LoadMain());
    }

    // 설정 패널 - 저장하기
    public void OnClickSaveBtn()
    {
        SoundManager.instance.SFXPlay("buttonSound");
        DatabaseManager.Instance.SaveData();
        settingPanel.SetActive(false);
    }

    // UI Canvas On/Off
    public void UICanvasSetActive()
    {
        if (uiCanvas.sortingOrder == 1)
        {
            uiCanvas.sortingOrder = -1;
        }
        else
        {
            uiCanvas.sortingOrder = 1;
        }
    }

    // 아이템 클릭 시
    public void OnClickItem(GameObject _item)
    {
        // 인벤토리에 추가
        Inventory.Instance.AcquireItem(_item.GetComponent<Item>());
        // 화면에 있는 아이템 삭제
        Destroy(_item);
    }

    private IEnumerator LoadMain()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Main");
    }

    // 트릭 성공 시 이펙트

    public void FadeInOut()
    {
        StartCoroutine(DoFadeInOut());
    }
    private IEnumerator DoFadeInOut()
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
        // Game Over
        if (mentalPoint == 0)
        {
            gameoverPanel.SetActive(true);
        }
    }

    /// <summary>
    /// 디버그 로그 화면에 출력 코드
    /// </summary>
    string myLog;
    Queue myLogQueue = new Queue();
    GUIStyle guiStyle = new GUIStyle();

    // 설정할 로그 출력 영역의 위치와 크기
    Rect logArea = new Rect(10, Screen.height - 200, 1000, 400); // 왼쪽 아래 위치

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        string newString = "\n [" + type + "] : " + myLog;
        myLogQueue.Enqueue(newString);

        // 큐가 최대 줄 수를 초과하면 첫 줄을 제거합니다.
        if (myLogQueue.Count > 4)
        {
            myLogQueue.Dequeue();
        }

        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue(newString);
        }

        myLog = string.Empty;
        foreach (string mylog in myLogQueue)
        {
            myLog += mylog;
        }
    }

    void OnGUI()
    {
        guiStyle.normal.textColor = Color.white;
        guiStyle.wordWrap = true;
        guiStyle.fontSize = 30; // 큰 글씨 크기 설정

        GUILayout.BeginArea(logArea);
        GUILayout.Label(myLog, guiStyle);
        GUILayout.EndArea();
    }

}