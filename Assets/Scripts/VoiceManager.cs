using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Define;

public enum VoiceMode
{
    Slient,
    Screaming
}

public class VoiceManager : MonoBehaviour
{
    // ���� ���� VoiceManager �ν��Ͻ��� �� instance�� ��� �༮�� ����
    // ������ ���� private
    private static VoiceManager instance = null;

    private string sceneName = null;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ� �ı� X
        }
        else Destroy(gameObject);
    }

    // VoiceManager �ν��Ͻ��� �����ϴ� ������Ƽ
    public static VoiceManager Instance
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



    [Header("���̽� �г�")]
    public GameObject voicePanel;

    [Header("���̽� �����̴�")]
    public Slider voiceSlider;

    [Header("����ũ �ΰ���")]
    [Range(1f, 100f)]
    public float sensitivity = 100;

    [SerializeField]
    [Header("���ú�")]
    private float loudness = 0f;

    private AudioSource _audio;

    [SerializeField]
    [Header("���� ����")]
    private VoiceMode mode;

    [SerializeField]
    [Header("Ÿ�̸�")]
    private float timer;

    [Header("ȯû �̹����� [0]: ���̹�, [1]: ���̵���, [2]: ��������, [3]: CEO��")]
    public GameObject[] images;

    [SerializeField]
    [Header("����ũ On/Off")]
    private bool isOn = false;

    void Start()
    {
        _audio = GetComponent<AudioSource>();

        _audio.clip = Microphone.Start(null, true, 10, 44100);
        _audio.loop = true;
        _audio.mute = false;
        while (!(Microphone.GetPosition(null) > 0)) { }
        _audio.Stop();

        mode = VoiceMode.Slient;
        
    }

    void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;
        switch(mode)
        {
            case VoiceMode.Slient:
                loudness = 0f;
                break;

            case VoiceMode.Screaming:
                voicePanel.SetActive(true);
                loudness = GetAveragedVolume() * sensitivity;
                voiceSlider.value = loudness / 10;

                // �ð� �ʰ� OR ȯû �̺�Ʈ ����
                if (timer < 0 || loudness > 10)
                {
                    voicePanel.SetActive(false);
                    mode = VoiceMode.Slient;
                    timer = 5.0f;
                    if (loudness > 10)
                    {
                        ImageOff();
                        Debug.Log("ȯû �̺�Ʈ ����");
                        ToggleMic();
                    }
                    else
                    {
                        ImageOff();
                        GameManager.Instance.MentalBreak();
                        Debug.Log("ȯû �̺�Ʈ ���� : ���ŷ� ����Ʈ -1");
                        ToggleMic();
                    }
                    SoundManager.instance.StopEventBGM(sceneName);

                }
                else
                {
                    timer -= Time.deltaTime;
                    Debug.Log("�Ҹ��� ���� ȯû�� �������");
                }
                break;
        }
    }
    public void ScreamingMode(RoomName roomName)
    {
        SoundManager.instance.PlayEventBGM();
        mode = VoiceMode.Screaming;
        images[(int)roomName].SetActive(true);
        // ����ũ On
        ToggleMic();
        switch (roomName)
        {
            case RoomName.Kid:
            {
                // ���̹� ȯû ���� ���
                SoundManager.instance.SFXPlay("bearCut");
                break;
            }
                
            case RoomName.Idol:
            {
                // ���̵��� ȯû ���� ���
                SoundManager.instance.SFXPlay("posterEvent");
                break;
            }

            case RoomName.Researcher:
            {
                // �������� ȯû ���� ���
                SoundManager.instance.SFXPlay("researcherEvent");
                break;
            }

            case RoomName.CEO:
            {
                // CEO�� ȯû ���� ���
                SoundManager.instance.SFXPlay("deerScream");
                break;
            }
        }
    }
    public void ImageOff()
    {
        foreach (GameObject image in images)
        {
            image.SetActive(false);
        }
    }

    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

    public void ToggleMic()
    {
        isOn = !isOn; // ��� ����ũ ���� ����

        if (isOn)
        {
            _audio.Play(); // ����ũ�� �� �� ����� ���
        }
        else
        {
            _audio.Stop(); // ����ũ�� �� �� ����� ����
        }
    }
}