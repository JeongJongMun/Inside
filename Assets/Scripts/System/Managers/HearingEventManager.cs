using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class HearingEventManager : MonoBehaviour
{
#region Private Variables
    [SerializeField] private float timer = 10.0f;
    [SerializeField] private bool isEvent = false;
    private InGameManager inGameManager;
    private SoundManager soundManager;
    private MicrophoneManager micManager;
    private RoomManager roomManager;
    private Dictionary<Define.RoomName, GameObject> eventImages = new Dictionary<Define.RoomName, GameObject>();
    private Define.RoomName eventRoom = Define.RoomName.None;
#endregion

#region Public Variables
    public Transform eventImageHolder;
    public GameObject hearingPanel;
    public Slider slider;
#endregion

#region Private Methods
    private void Awake()
    {
        micManager = FindObjectOfType<MicrophoneManager>();
        inGameManager = FindObjectOfType<InGameManager>();
        roomManager = FindObjectOfType<RoomManager>();
        soundManager = GameManager.instance.soundManager;
        foreach (Transform image in eventImageHolder) {
            eventImages.Add((Define.RoomName)Enum.Parse(typeof(Define.RoomName), image.gameObject.name), image.gameObject);
        }
    }
    private void Update()
    {
        if (!isEvent) return;
        float _loudness = micManager.loudness;
        slider.value = _loudness / 10;
        timer -= Time.deltaTime;

        if (timer <= 0 || _loudness >= 10) {
            soundManager.Play(roomManager.CurrentRoomName().ToString(), SoundType.BGM);
            micManager.ToggleMic();
            eventImages[eventRoom].SetActive(false);
            hearingPanel.SetActive(false);
            isEvent = false;

            if (timer <= 0) {
                inGameManager.BlinkingEffect(Color.red);
                inGameManager.MentalBreak();
            }
            timer = 10.0f;
        }
    }
#endregion

#region Public Methods
    public void OnHearingEvent(Define.RoomName roomName)
    {
        eventRoom = roomName;
        inGameManager.BlinkingEffect(Color.black);
        eventImages[roomName].SetActive(true);
        hearingPanel.SetActive(true);
        soundManager.Play($"Hearing{roomName}", SoundType.BGM);
        micManager.ToggleMic();
        isEvent = true;
    }
#endregion
}