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
    private static VoiceManager instance = null;

    private string sceneName = null;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

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



    public GameObject voicePanel;

    public Slider voiceSlider;

    [Range(1f, 100f)]
    public float sensitivity = 100;

    [SerializeField]
    private float loudness = 0f;

    private AudioSource _audio;

    [SerializeField]
    private VoiceMode mode;

    [SerializeField]
    private float timer;

    public GameObject[] images;

    [SerializeField]
    private bool isOn = false;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        mode = VoiceMode.Slient;
        foreach (string device in Microphone.devices)
        {
            Debug.Log(device);
        }
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

                if (timer < 0 || loudness > 10)
                {
                    voicePanel.SetActive(false);
                    mode = VoiceMode.Slient;
                    timer = 10.0f;
                    if (loudness > 10)
                    {
                        ImageOff();
                    }
                    else
                    {
                        ImageOff();
                        InGameManager.Instance.MentalBreak();
                    }
                    InGameManager.Instance.FadeInOut();
                    ToggleMic();
                    // SoundManager.instance.StopEventBGM(sceneName);

                }
                else
                {
                    timer -= Time.deltaTime;
                }
                break;
        }
    }
    public void ScreamingMode(RoomName roomName)
    {
        InGameManager.Instance.FadeInOut();
        // SoundManager.instance.PlayEventBGM();
        mode = VoiceMode.Screaming;
        images[(int)roomName].SetActive(true);
        ToggleMic();
        switch (roomName)
        {
            case RoomName.Kid:
            {
                SoundManager.instance.SFXPlay("bearCut");
                break;
            }
                
            case RoomName.Idol:
            {
                SoundManager.instance.SFXPlay("posterEvent");
                break;
            }

            case RoomName.Researcher:
            {
                SoundManager.instance.SFXPlay("researcherEvent");
                break;
            }

            case RoomName.CEO:
            {
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

    [ContextMenu("ToggleMicrophone")]
    private void ToggleMic()
    {
        isOn = !isOn;

        if (isOn)
        {
            _audio.clip = Microphone.Start(null, true, 10, 44100);
            _audio.loop = true;
            _audio.mute = false;
            while (!(Microphone.GetPosition(null) > 0)) { }
            _audio.Play();

        }
        else
        {
            Microphone.End(null);
            _audio.clip = null;
            _audio.Stop();
        }
    }
}