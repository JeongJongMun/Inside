using System.Collections.Generic;
using UnityEngine;
/* SoundManager.cs
 * - 사운드 파일을 로드하고 재생하는 기능 담당
 * - 배경음악, 효과음 재생
 *
 * Audio Source - 소리 발생 근원지
 * Audio Listener - 소리 수신 근원지(Main Camera에 자동 부착)
 * Audio Clip - 소리 파일
*/
// TODO: SoundManager 개선 - 모든 버튼을 찾아 자동으로 이벤트 등록
public enum SoundType
{
    BGM,        // 배경음
    EFFECT,     // 효과음
    MAXCOUNT    // 최대 개수
}
public class SoundManager
{    
#region Private Variables
    private AudioSource[] audioSources = new AudioSource[(int)SoundType.MAXCOUNT];
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>(); // 사운드 파일을 저장할 딕셔너리 <경로, 해당 오디오 클립> -> Object Pooling
#endregion

#region Private Methods
	private AudioClip GetOrAddAudioClip(string path, SoundType type = SoundType.EFFECT)
    {
        // EFFECT/Clock
        // BGM/0. Kid Room
        path = $"{type}/{path}";

        // Sounds/EFFECT/Clock
        // Sounds/BGM/0. Kid Room
		if (path.Contains("Sounds/") == false)
			path = $"Sounds/{path}"; // Sounds 폴더 안에 저장될 수 있도록

		AudioClip audioClip = null;

        // BGM 배경음악 클립 붙이기
		if (type == SoundType.BGM) {
			audioClip = Resources.Load<AudioClip>(path);
		}
        // Effect 효과음 클립 붙이기
		else {
			if (audioClips.TryGetValue(path, out audioClip) == false) {
				audioClip = Resources.Load<AudioClip>(path);
				audioClips.Add(path, audioClip);
			}
		}

		if (audioClip == null)
			Debug.LogFormat("[SoundManager] 오디오 클립이 없습니다: {0}", path);

		return audioClip;
    }
#endregion

#region Public Methods
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null) {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(SoundType)); // "BGM", "EFFECT"
            for (int i = 0; i < soundNames.Length - 1; i++) {
                GameObject go = new GameObject { name = soundNames[i] }; 
                audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            audioSources[(int)SoundType.BGM].loop = true;       // bgm 재생기는 무한 반복 재생
            audioSources[(int)SoundType.BGM].volume = 0.5f;
        }
    }
    public void Clear()
    {
        // 재생기 전부 재생 스탑, 음반 빼기
        foreach (AudioSource audioSource in audioSources) {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // 효과음 Dictionary 비우기
        audioClips.Clear();
    }
    public void Play(AudioClip audioClip, SoundType type = SoundType.EFFECT, float pitch = 1.0f, float volume = 1.0f)
	{
        if (audioClip == null)
            return;

        // BGM 배경음악 재생
		if (type == SoundType.BGM) {
			AudioSource audioSource = audioSources[(int)type];
            if (audioSource == null)
                return;
			if (audioSource.isPlaying)
				audioSource.Stop();

			audioSource.pitch = pitch;
			audioSource.clip = audioClip;
			audioSource.Play();
		}
        // Effect 효과음 재생
		else {
			AudioSource audioSource = audioSources[(int)type];
            if (audioSource == null)
                return;
			audioSource.pitch = pitch;
            audioSource.volume = volume;
			audioSource.PlayOneShot(audioClip);
		}
	}

    public void Play(string path, SoundType type = SoundType.EFFECT, float pitch = 1.0f, float volume = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch, volume);
    }

    public void Stop(AudioClip audioClip, SoundType type = SoundType.EFFECT)
    {
        if (audioClip == null)
            return;

        if (type == SoundType.BGM) // BGM 배경음악 정지
        {
            AudioSource audioSource = audioSources[(int)SoundType.BGM];
            if (audioSource.clip == audioClip)
            {
                audioSource.Stop();
                audioSource.clip = null;
            }
        }
        else // Effect 효과음 정지
        {
            AudioSource audioSource = audioSources[(int)SoundType.EFFECT];
            if (audioSource.clip == audioClip)
            {
                audioSource.Stop();
                audioSource.clip = null;
            }
        }
    }

    public void Stop(string path, SoundType type = SoundType.EFFECT)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Stop(audioClip, type);
    }

    public void Stop(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    public void StopAll()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
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
    public void SFXPlay(string _soundName)
    {
    }
}