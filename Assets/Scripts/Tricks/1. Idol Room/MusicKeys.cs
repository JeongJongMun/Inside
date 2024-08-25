using UnityEngine;
using UnityEngine.UI;

public class MusicKeys : MonoBehaviour
{
    private SoundManager soundManager;
    private void Start()
    {
        soundManager = GameManager.instance.soundManager;
        if (TryGetComponent(out Button button)) {
            button.onClick.AddListener(() => soundManager.Play("piano" + this.name)); // TODO: 소리 너무 작음
        }
    }
}