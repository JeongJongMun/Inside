using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditMove : MonoBehaviour
{
    private RectTransform recttransform;
    
    [Header("크레딧 시작 여부")]
    public bool isStart = false;

    [Header("크레딧 이동 속도")]
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
