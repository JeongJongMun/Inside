using UnityEngine;
using UnityEngine.UI;

public class IdolRoomDressingTable : Trick
{
    private float brokenTime = 3.0f;

    [Header("ȭ��� Ȯ�� �г�")]
    public GameObject dressingTableZoomPanel;

    [Header("Ʈ�� Ǯ�� Ȯ�� �� ȭ��� �̹��� ������ �̹���")]
    public Image dressingTableZoom;

    [Header("Ȯ�� �� �μ��� ȭ��� �̹���")]
    public Sprite brokenDressingTableZoom;

    [Header("Ȯ�� ������ �� �μ��� ȭ��� �̹���")]
    public GameObject brokenDressingTable;

    [Header("�Ƿ翧")]
    public GameObject silhouette;

    [Header("�Ƿ翧 �ִϸ��̼� �ӵ�")]
    public float pulseSpeed = 3.0f;

    [Header("�Ƿ翧 �ִϸ��̼� ����")]
    public float pulseAmplitude = 0.5f;

    private Image silhouetteImage;
    private float timeOffset;

    public override void Start()
    {
        base.Start();
        silhouetteImage = silhouette.GetComponent<Image>();
        timeOffset = Random.Range(0f, 10f); // To avoid all objects pulsating in sync
    }

    private void Update()
    {
        if (!IsSolved() && dressingTableZoomPanel.activeSelf)
        {
            // TrySolve(this.gameObject);
        }
    }
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (brokenTime < 0f)
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("breakMirror"); // break Mirror Sound
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
        dressingTableZoom.sprite = brokenDressingTableZoom;
        silhouette.SetActive(false);
        brokenDressingTable.SetActive(true);
        Debug.Log("ȭ��� SolvedAction");
    }

    private void SilhouetteAnimation()
    {
        float t = (Mathf.Sin((Time.time + timeOffset) * pulseSpeed) + 1) * 0.5f * pulseAmplitude + 0.5f;
        Color targetColor = Color.Lerp(Color.clear, Color.white, t);
        silhouetteImage.color = targetColor;
    }
}
