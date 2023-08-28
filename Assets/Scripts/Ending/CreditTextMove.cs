using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditTextMove : MonoBehaviour
{
    private RectTransform recttransform;

    [SerializeField]
    [Header("ũ���� �̵� �ӵ�")]
    [Range(0.1f, 1f)]
    private float speed = 1;

    void Start()
    {
        recttransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        recttransform.position += Vector3.up * speed;
        Debug.LogFormat("ũ���� �ӵ�:{0}", speed);
        if (recttransform.anchoredPosition.y > 1500) SceneManager.LoadScene("Main");
    }
}
