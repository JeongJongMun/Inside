using UnityEngine;

public class MicrophoneManager : MonoBehaviour
{
#region Private Variables
    private AudioSource audioSource;
    [SerializeField] private bool isMicOn = false;
    [SerializeField, Range(1f, 100f)] private float sensitivity = 100;
#endregion

#region Public Variables
    public float loudness = 0f;
#endregion

#region Private Methods
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (string device in Microphone.devices) {
            Debug.Log(device);
        }
    }
    private void Update()
    {
        if (!isMicOn) return;

        float[] data = new float[256];
        float a = 0;
        audioSource.GetOutputData(data, 0);
        foreach (float s in data) {
            a += Mathf.Abs(s);
        }
        loudness = a / 256 * sensitivity;
    }
#endregion

#region Public Methods
    public void ToggleMic()
    {
        isMicOn = !isMicOn;

        if (isMicOn) {
            audioSource.clip = Microphone.Start(null, true, 10, 44100);
            audioSource.loop = true;
            audioSource.mute = false;
            while (!(Microphone.GetPosition(null) > 0)) { }
            audioSource.Play();

        }
        else {
            Microphone.End(null);
            audioSource.clip = null;
            audioSource.Stop();
        }
    }
#endregion
}