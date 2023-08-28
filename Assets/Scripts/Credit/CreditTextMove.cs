using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditTextMove : MonoBehaviour
{
    private RectTransform recttransform;

    [SerializeField]
    [Header("农饭调 捞悼 加档")]
    [Range(0.1f, 1f)]
    private float speed = 1;

    void Start()
    {
        recttransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        recttransform.position += Vector3.up * speed;
        Debug.LogFormat("农饭调 加档:{0}", speed);
        if (recttransform.anchoredPosition.y > 1500) SceneManager.LoadScene("Main");
    }
}
