using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // ���� ���� GameManager �ν��Ͻ��� �� instance�� ��� �༮�� ����
    // ������ ���� private
    private static GameManager instance = null;

    //For Audio volume Slider
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Slider audioSlider;

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


    [Header("���ŷ� ����Ʈ")]
    public int mentalPoint = 3;

    [Header("���ŷ� ����Ʈ �̹��� �迭")]
    public GameObject[] mentalImage;

    [Header("����â �г�")]
    public GameObject settingPanel;

    [Header("���ӿ��� �г�")]
    public GameObject gameoverPanel;

    [Header("Ʈ�� ���� ����Ʈ")]
    public Image fadeImage;

    [SerializeField]
    [Header("����Ʈ �ӵ�")]
    [Range(0.01f, 10f)]
    private float fadeTime;

    [Header("UI Canvas")]
    public Canvas uiCanvas;


    // ���� ��ư -> ���� �г� ON
    public void SettingPanelOnOff()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }
    // ���� �г� - �������� & ���ӿ��� �г� - ��������
    public void OnClickExitBtn(GameObject panel)
    {
        panel.SetActive(false);
        SoundManager.instance.SFXPlay("buttonSound");
        UICanvasSetActive();
        StartCoroutine(LoadMain());
    }

    // ���� �г� - �����ϱ�
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

    // ������ Ŭ�� ��
    public void OnClickItem(GameObject _item)
    {
        // �κ��丮�� �߰�
        Inventory.Instance.AcquireItem(_item.GetComponent<Item>());
        // ȭ�鿡 �ִ� ������ ����
        Destroy(_item);
    }

    private IEnumerator LoadMain()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Main");
    }

    // Ʈ�� ���� �� ����Ʈ

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
        // Game Over
        if (mentalPoint == 0)
        {
            gameoverPanel.SetActive(true);
        }
    }

    /// <summary>
    /// ����� �α� ȭ�鿡 ��� �ڵ�
    /// </summary>
    string myLog;
    Queue myLogQueue = new Queue();
    GUIStyle guiStyle = new GUIStyle();

    // ������ �α� ��� ������ ��ġ�� ũ��
    Rect logArea = new Rect(10, Screen.height - 200, 1000, 400); // ���� �Ʒ� ��ġ

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

        // ť�� �ִ� �� ���� �ʰ��ϸ� ù ���� �����մϴ�.
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
        guiStyle.fontSize = 30; // ū �۾� ũ�� ����

        GUILayout.BeginArea(logArea);
        GUILayout.Label(myLog, guiStyle);
        GUILayout.EndArea();
    }

}