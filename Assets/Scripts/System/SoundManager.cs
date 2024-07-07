using UnityEngine;
using UnityEngine.SceneManagement;
/* SoundManager.cs
 * - 사운드 파일을 로드하고 재생하는 기능 담당
 * - 배경음악, 효과음 재생
 *
 * Audio Source - 소리 발생 근원지
 * Audio Listener - 소리 수신 근원지(Main Camera에 자동 부착)
 * Audio Clip - 소리 파일
*/
public enum SoundType
{
    BGM,        // 배경음
    EFFECT,     // 효과음
    MAXCOUNT    // 최대 개수
}
public class SoundManager : MonoBehaviour
{
    
    // TODO: Main 배경음 재생
    // TODO: SoundManager 개선 - 모든 버튼을 찾아 자동으로 이벤트 등록

#region Private Variables

#endregion

#region Public Variables

#endregion

#region Private Methods

#endregion

#region Public Methods

#endregion

    [Header("---------Audio Source----------")]
    [SerializeField] public AudioSource bgmSound;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource pianoSource;

    [Header("---------Audio Clip----------")]
    public AudioClip[] bgmList;
    public AudioClip[] SFXList;
    public AudioClip[] pianoList;

    public static SoundManager instance = null;

    private string sceneName = null;

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    public void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i=0;i<bgmList.Length;i++) {
            if(arg0.name == bgmList[i].name)
                BgmSoundPlay(bgmList[i]);
        }
    }

    // 피아노 재생
    public void pianoPlay(string sfxName, int note)
    {
        toPianoPlay(sfxName, pianoList[note]);
    }

    public void toPianoPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        pianoSource.clip = clip;
        pianoSource.PlayOneShot(clip);

        Destroy(go, clip.length);
    }

    // 효과음 재생
    public void SFXPlay(string sfxName)
    {
        for(int i=0;i<SFXList.Length;i++) {
            if(sfxName == SFXList[i].name)
                toSFXPlay(sfxName, SFXList[i]);
        }
    }
    
    public void toSFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        SFXSource.clip = clip;
        SFXSource.PlayOneShot(clip);
        
        Destroy(go, clip.length);
    }
    // 아이돌방 오르골 브금 재생 및 배갱 브금 정지
    public void OnMusicbox()
    {
        bgmSound.Stop();
        bgmSound.clip = bgmList[6];
        bgmSound.Play();
    }

    // 아이돌방 오르골 브금 정지 및 배경 브금 재생
    public void OutMusicbox()
    {
        bgmSound.Stop();
        bgmSound.clip = bgmList[2];
        bgmSound.Play();
    }

    // 환청 이벤트 BGM 재생
    public void PlayEventBGM()
    {
        bgmSound.Stop();
        bgmSound.clip = bgmList[9];
        bgmSound.Play();
    }
    
    // 환청 이벤트 BGM 중지 후 원래 방 배경음 재생
    public void StopEventBGM(string bgmName)
    {
        bgmSound.Stop();
        for(int i=0;i<bgmList.Length;i++) {
            if(bgmName == bgmList[i].name)
                bgmSound.clip = bgmList[i];
        }
        bgmSound.Play();
    }

    // 배경음악 재생
    private void BgmSoundPlay(AudioClip clip)
    {
        bgmSound.clip = clip;
        bgmSound.loop = true;
        bgmSound.Play();
    }
}