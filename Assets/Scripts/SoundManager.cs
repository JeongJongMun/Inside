using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [Header("---------Audio Source----------")]
    [SerializeField] AudioSource bgmSound;
    [SerializeField] AudioSource SFXSource;

    [Header("---------Audio Clip----------")]
    public AudioClip[] bgmList;
    public AudioClip[] SFXList;
    

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
    
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i=0;i<bgmList.Length;i++) {
            if(arg0.name == bgmList[i].name)
                BgmSoundPlay(bgmList[i]);
        }
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

    // 배경음악 재생
    public void BgmSoundPlay(AudioClip clip)
    {
        bgmSound.clip = clip;
        bgmSound.loop = true;
        bgmSound.Play();
    }
}
