using UnityEngine;

public class LaptopBackground : MonoBehaviour
{

    public bool isEyesMoving = false;

    [Header("Ä«Ä«¿ÀÅå ¹è°æ")]
    public GameObject laptopKakao;

    [Header("°õµ¹ÀÌ ´«")]
    public GameObject[] eyes;

    [SerializeField]
    [Header("ÇöÀç °õµ¹ÀÌ ´« ÁÂÇ¥")]
    private RectTransform[] positions;

    [SerializeField]
    [Header("Å¸ÀÌ¸Ó")]
    private float timer = -0.5f;

    [SerializeField]
    [Header("°õµ¹ÀÌ ´« ½ÃÀÛ º¤ÅÍ")]
    private Vector2[] startVector;

    [SerializeField]
    [Header("°õµ¹ÀÌ ´« ¸ñÀûÁö º¤ÅÍ")]
    private Vector2[] destinationVector;

    private void Start()
    {
        startVector = new Vector2[2];
        destinationVector = new Vector2[2];
        positions = new RectTransform[2];

        startVector[0] = eyes[0].GetComponent<RectTransform>().anchoredPosition;
        startVector[1] = eyes[1].GetComponent<RectTransform>().anchoredPosition;

        destinationVector[0] = startVector[0] + Vector2.left * 10;
        destinationVector[1] = startVector[1] + Vector2.right * 10;

        positions[0] = eyes[0].GetComponent<RectTransform>();
        positions[1] = eyes[1].GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (isEyesMoving)
        {
            timer += Time.deltaTime;
            if (timer <= 3.0f)
            {
                positions[0].anchoredPosition = Vector2.Lerp(startVector[0], destinationVector[0], timer / 3.0f);
                positions[1].anchoredPosition = Vector2.Lerp(startVector[1], destinationVector[1], timer / 3.0f);
            }
            if (timer > 4.0f)
            {
                laptopKakao.SetActive(true);
            }
        }
    }
}
