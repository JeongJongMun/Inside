using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditMove : MonoBehaviour
{
    private RectTransform recttransform;

    [Header("크레딧 이동 속도")]
    private float speed = 3f;

    void Start()
    {
        recttransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        recttransform.anchoredPosition += Vector2.up * speed;
        if (recttransform.anchoredPosition.y > 1500) SceneManager.LoadScene("Main");
    }
}
