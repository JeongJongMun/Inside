using UnityEngine;
using UnityEngine.UI;

public class MusicKeys : MonoBehaviour
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.instance;
        GetComponent<Button>().onClick.AddListener(() => OnClickKey(int.Parse(name)));
    }

    private void OnClickKey(int key)
    {
        gameManager.soundManager.Play("piano" + key.ToString()); // TODO: 소리 너무 작음
    }
}
