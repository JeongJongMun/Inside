using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditMove : MonoBehaviour
{
    private RectTransform recttransform;
    
    [Header("ũ���� ���� ����")]
    public bool isStart = false;

    [Header("ũ���� �̵� �ӵ�")]
    public float speed;

    void Start()
    {
        recttransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (isStart) recttransform.anchoredPosition += Vector2.up * speed;
        if (recttransform.anchoredPosition.y > 1500) SceneManager.LoadScene("Main");
    }
}
