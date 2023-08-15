using UnityEngine;
using UnityEngine.UI;

public class IdolRoomDressingTable : Trick
{
    private float brokenTime = 3.0f;

    [Header("화장대 확대 패널")]
    public GameObject dressingTableZoomPanel;

    [Header("트릭 풀면 확대 시 화장대 이미지 변경할 이미지")]
    public Image dressingTableZoom;

    [Header("확대 시 부서진 화장대 이미지")]
    public Sprite brokenDressingTableZoom;

    [Header("확대 안했을 때 부서진 화장대 이미지")]
    public GameObject brokenDressingTable;

    [Header("실루엣")]
    public GameObject silhouette;

    [Header("실루엣 애니메이션 속도")]
    public float pulseSpeed = 3.0f;

    [Header("실루엣 애니메이션 진폭")]
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
        dressingTableZoom.sprite = brokenDressingTableZoom;
        silhouette.SetActive(false);
        brokenDressingTable.SetActive(true);
        Debug.Log("화장대 SolvedAction");
    }

    private void SilhouetteAnimation()
    {
        float t = (Mathf.Sin((Time.time + timeOffset) * pulseSpeed) + 1) * 0.5f * pulseAmplitude + 0.5f;
        Color targetColor = Color.Lerp(Color.clear, Color.white, t);
        silhouetteImage.color = targetColor;
    }
}
