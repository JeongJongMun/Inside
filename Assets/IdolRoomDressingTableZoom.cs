using UnityEngine;
using UnityEngine.UI;

public class IdolRoomDressingTableZoom : Trick
{
    private float brokenTime = 3.0f;

    [Header("�μ��� ȭ��� �̹���")]
    public Sprite brokenDressingTable;

    [Header("�Ƿ翧")]
    public GameObject silhouette;

    [Header("�Ƿ翧 �ִϸ��̼� �ӵ�")]
    public float pulseSpeed = 3.0f;

    [Header("�Ƿ翧 �ִϸ��̼� ����")]
    public float pulseAmplitude = 0.5f;

    private Image silhouetteImage;
    private float timeOffset;

    private void Start()
    {
        silhouetteImage = silhouette.GetComponent<Image>();
        timeOffset = Random.Range(0f, 10f); // To avoid all objects pulsating in sync
    }

    private void Update()
    {
        if (!IsSolved())
        {
            TrySolve(this.gameObject);
        }
    }
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (brokenTime < 0f)
            {
                Debug.LogFormat("{0} Solved", this.name);
                Solved();
                SolvedAction();
            }
            else
            {
                SilhouetteAnimation();
                brokenTime -= Time.deltaTime;
            }
        }
    }
    public override void SolvedAction()
    {
        GetComponent<Image>().sprite = brokenDressingTable;
        silhouette.SetActive(false);
    }

    private void SilhouetteAnimation()
    {
        Debug.Log("�Ƿ翧 �ִϸ��̼� ��");
        float t = (Mathf.Sin((Time.time + timeOffset) * pulseSpeed) + 1) * 0.5f * pulseAmplitude + 0.5f;
        Color targetColor = Color.Lerp(Color.clear, Color.white, t);
        silhouetteImage.color = targetColor;
    }
}
