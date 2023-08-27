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
        sceneName = SceneManager.GetActiveScene().name;
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

    void Start()
    {
        _audio = GetComponent<AudioSource>();

        _audio.clip = Microphone.Start(null, true, 10, 44100);
        _audio.loop = true;
        _audio.mute = false;
        while (!(Microphone.GetPosition(null) > 0)) { }
        _audio.Play();

        mode = VoiceMode.Slient;
        
    }
    void Update()
    {
        switch(mode)
        {
            case VoiceMode.Slient:
                loudness = 0f;
                break;

            case VoiceMode.Screaming:
                voicePanel.SetActive(true);
                loudness = GetAveragedVolume() * sensitivity;
                voiceSlider.value = loudness / 10;

                // �ð� �ʰ� OR ȯû ����� ����
                if (timer < 0 || loudness > 10)
                {
                    voicePanel.SetActive(false);
                    mode = VoiceMode.Slient;
                    timer = 5.0f;
                    if (loudness > 10)
                    {
                        ImageOff();
                        Debug.Log("ȯû ����� ����");
                    }
                    else
                    {
                        ImageOff();
                        GameManager.Instance.MentalBreak();
                        Debug.Log("ȯû ����� ���� : ���ŷ� ����Ʈ -1");
                    }
                    SoundManager.instance.BgmSoundRePlay(sceneName);

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
        SoundManager.instance.BgmSoundStop(sceneName);
        mode = VoiceMode.Screaming;
        images[(int)roomName].SetActive(true);
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
}
