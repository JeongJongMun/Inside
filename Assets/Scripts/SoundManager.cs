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

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
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

    // 아이돌방 오르골 재생 후 아래화살표 눌렀을 때 오르골 정지, 배경음 재생
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

    // 배경음악 뮤트 해제
    public void BgmSoundRePlay(string bgmName)
    {
        for(int i=0;i<bgmList.Length;i++) {
            if(bgmName == bgmList[i].name)
                bgmSound.clip = bgmList[i];
        }
        bgmSound.mute = false;
    }

    // 배경음악 뮤트(=정지)
    public void BgmSoundStop(string bgmName)
    {
        for(int i=0;i<bgmList.Length;i++) {
            if(bgmName == bgmList[i].name)
                bgmSound.clip = bgmList[i];
        }
        bgmSound.mute = true;
    }
}
