using UnityEngine;
using UnityEngine.UI;

public class MusicKeys : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => OnClickKey(int.Parse(name)));
    }

    private void OnClickKey(int key)
    {
        // SoundManager.instance.pianoPlay("piano", key);
    }
}
