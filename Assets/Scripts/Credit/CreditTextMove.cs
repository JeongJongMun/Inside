using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditTextMove : MonoBehaviour
{
    private RectTransform recttransform;

    [SerializeField]
    [Header("ũ���� �̵� �ӵ�")]
    private float speed = 1;

    void Start()
    {
        recttransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        recttransform.anchoredPosition += Vector2.up * speed;
        if (recttransform.anchoredPosition.y > 1500) SceneManager.LoadScene("Main");
    }
}
