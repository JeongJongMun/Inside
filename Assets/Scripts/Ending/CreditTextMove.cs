using UnityEngine;

public class CreditTextMove : MonoBehaviour
{
    private RectTransform recttransform;

    [SerializeField]
    [Header("크레딧 이동 속도")]
    [Range(0.1f, 1f)]
    private float speed;

    void Start()
    {
        recttransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        recttransform.position += Vector3.up * speed;
    }
}
