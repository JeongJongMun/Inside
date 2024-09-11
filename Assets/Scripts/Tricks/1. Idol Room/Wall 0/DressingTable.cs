using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DressingTable : NewTrick
{
#region Private Variables
    private const float brokenTime = 3.0f;
    private float pulseSpeed = 3.0f;
    private float pulseAmplitude = 0.5f;
    private Image silhouetteImage;
    private float timeOffset = 1.0f;
    private GameManager gameManager;
    private RoomManager roomManager;
#endregion

#region Public Variables
    public GameObject[] brokenMarks;
    public GameObject silhouette;
#endregion

#region Private Methods
    private void Awake()
    {
        roomManager = FindObjectOfType<RoomManager>();
        gameManager = GameManager.instance;
    }
    private void OnEnable() 
    {
        if (IsComplete) return;
        Managers.Sound.Play("Silhouette", SoundType.BGM);
        InvokeRepeating("SilhouetteAnimation", 0f, 0.05f);
        StartCoroutine(CheckCompleteCoroutine(brokenTime));
    }
    private IEnumerator CheckCompleteCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        IsComplete = CheckComplete(null);
    }
    private void OnDisable() 
    {
        if (!IsComplete) {
            Managers.Sound.Play(roomManager.CurrentRoomName().ToString(), SoundType.BGM);
        }
        silhouetteImage.color = Color.white;
        CancelInvoke();
    }
    private void SilhouetteAnimation()
    {
        float t = (Mathf.Sin((Time.time + timeOffset) * pulseSpeed) + 1) * 0.5f * pulseAmplitude + 0.5f;
        Color targetColor = Color.Lerp(Color.clear, Color.white, t);
        silhouetteImage.color = targetColor;
    }
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
        silhouetteImage = silhouette.GetComponent<Image>();
        timeOffset = Random.Range(0f, 10f); // To avoid all objects pulsating in sync
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        Managers.Sound.Play("breakMirror");
        Managers.Sound.Play(roomManager.CurrentRoomName().ToString(), SoundType.BGM);
        CancelInvoke();
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        silhouette.SetActive(false);
        foreach (GameObject brokenMark in brokenMarks) {
            brokenMark.SetActive(true);
        }
    }
#endregion
}