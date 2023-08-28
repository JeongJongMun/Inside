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
    // 게임 내에 VoiceManager 인스턴스는 이 instance에 담긴 녀석만 존재
    // 보안을 위해 private
    private static VoiceManager instance = null;

    private string sceneName = null;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에 파괴 X
        }
        else Destroy(gameObject);
    }

    // VoiceManager 인스턴스에 접근하는 프로퍼티
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



    [Header("보이스 패널")]
    public GameObject voicePanel;

    [Header("보이스 슬라이더")]
    public Slider voiceSlider;

    [Header("마이크 민감도")]
    [Range(1f, 100f)]
    public float sensitivity = 100;

    [SerializeField]
    [Header("데시벨")]
    private float loudness = 0f;

    private AudioSource _audio;

    [SerializeField]
    [Header("현재 상태")]
    private VoiceMode mode;

    [SerializeField]
    [Header("타이머")]
    private float timer;

    [Header("환청 이미지들 [0]: 아이방, [1]: 아이돌방, [2]: 연구원방, [3]: CEO방")]
    public GameObject[] images;

    [SerializeField]
    [Header("마이크 On/Off")]
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

                // 시간 초과 OR 환청 이벤트 성공
                if (timer < 0 || loudness > 10)
                {
                    voicePanel.SetActive(false);
                    mode = VoiceMode.Slient;
                    timer = 5.0f;
                    if (loudness > 10)
                    {
                        ImageOff();
                        Debug.Log("환청 이벤트 성공");
                        ToggleMic();
                    }
                    else
                    {
                        ImageOff();
                        GameManager.Instance.MentalBreak();
                        Debug.Log("환청 이벤트 실패 : 정신력 포인트 -1");
                        ToggleMic();
                    }
                    SoundManager.instance.StopEventBGM(sceneName);

                }
                else
                {
                    timer -= Time.deltaTime;
                    Debug.Log("소리를 질러 환청을 무찌르세요");
                }
                break;
        }
    }
    public void ScreamingMode(RoomName roomName)
    {
        SoundManager.instance.PlayEventBGM();
        mode = VoiceMode.Screaming;
        images[(int)roomName].SetActive(true);
        // 마이크 On
        ToggleMic();
        switch (roomName)
        {
            case RoomName.Kid:
            {
                // 아이방 환청 사운드 재생
                SoundManager.instance.SFXPlay("bearCut");
                break;
            }
                
            case RoomName.Idol:
            {
                // 아이돌방 환청 사운드 재생
                SoundManager.instance.SFXPlay("posterEvent");
                break;
            }

            case RoomName.Researcher:
            {
                // 연구원방 환청 사운드 재생
                SoundManager.instance.SFXPlay("researcherEvent");
                break;
            }

            case RoomName.CEO:
            {
                // CEO방 환청 사운드 재생
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
        isOn = !isOn; // 토글 마이크 상태 변경

        if (isOn)
        {
            _audio.Play(); // 마이크를 켤 때 오디오 재생
        }
        else
        {
            _audio.Stop(); // 마이크를 끌 때 오디오 정지
        }
    }
}