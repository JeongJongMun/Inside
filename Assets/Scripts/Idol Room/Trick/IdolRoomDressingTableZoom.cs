using UnityEngine;
using UnityEngine.UI;

public class IdolRoomDressingTableZoom : Trick
{
    private float brokenTime = 3.0f;

    [Header("확대 시 부서진 화장대 이미지")]
    public Sprite brokenDressingTable;

    [Header("확대 안했을 때 부서진 화장대 이미지")]
    public GameObject dressingTableBroken;

    [Header("실루엣")]
    public GameObject silhouette;

    [Header("실루엣 애니메이션 속도")]
    public float pulseSpeed = 3.0f;

    [Header("실루엣 애니메이션 진폭")]
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
