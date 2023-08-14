using UnityEngine;
using UnityEngine.UI;

public class IdolRoomDressingTableZoom : Trick
{
    private float brokenTime = 3.0f;

    [Header("Ȯ�� �� �μ��� ȭ��� �̹���")]
    public Sprite brokenDressingTable;

    [Header("Ȯ�� ������ �� �μ��� ȭ��� �̹���")]
    public GameObject dressingTableBroken;

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
                SetIsSolved(true);
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
        dressingTableBroken.SetActive(true);
    }

    private void SilhouetteAnimation()
    {
        float t = (Mathf.Sin((Time.time + timeOffset) * pulseSpeed) + 1) * 0.5f * pulseAmplitude + 0.5f;
        Color targetColor = Color.Lerp(Color.clear, Color.white, t);
        silhouetteImage.color = targetColor;
    }
}
