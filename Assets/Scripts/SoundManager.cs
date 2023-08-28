using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [Header("---------Audio Source----------")]
    [SerializeField] AudioSource bgmSound;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource pianoSource;

    [Header("---------Audio Clip----------")]
    public AudioClip[] bgmList;
    public AudioClip[] SFXList;
    public AudioClip[] pianoList;

    private bool IsMusixbox = false;

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

    // 아이돌방 오르골 재생
    public void musixboxPlay()
    {
        bgmSound.Stop();
        bgmSound.clip = bgmList[6];
        IsMusixbox = true;
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

    // 오르골 효과음 재생 후 아래화살표 눌렀을 때 BGM 정지, 방 배경음 재생
    public void OnclickBottomArrow()
    {
        if(IsMusixbox)
        {
            bgmSound.Stop();
            bgmSound.clip = bgmList[2];
            bgmSound.Play();
            IsMusixbox = false;
        }
    }

    // 배경음악 재생
    private void BgmSoundPlay(AudioClip clip)
    {
        bgmSound.clip = clip;
        bgmSound.loop = true;
        bgmSound.Play();
    }
}