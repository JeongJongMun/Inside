using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DressingTable : NewTrick
{
#region Private Variables
    private const float brokenTime = 4.0f;
    private float pulseSpeed = 3.0f;
    private float pulseAmplitude = 0.5f;
    private Image silhouetteImage;
    private float timeOffset = 1.0f;
#endregion

#region Public Variables
    public GameObject[] brokenMarks;
    public GameObject silhouette;
#endregion

#region Private Methods
    private void OnEnable() 
    {
        if (IsComplete) return;
        InvokeRepeating("SilhouetteAnimation", 0f, 0.05f);
        StartCoroutine(CheckCompleteCoroutine(brokenTime));
    }
    private IEnumerator CheckCompleteCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        CheckComplete(null);
    }
    private void OnDisable() 
    {
        CancelInvoke();
        silhouetteImage.color = Color.white;
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
        GameManager.Instance.soundManager.Play("breakMirror");
        CancelInvoke();
        IsComplete = true;
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