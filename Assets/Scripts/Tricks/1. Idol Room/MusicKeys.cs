using UnityEngine;
using UnityEngine.UI;

public class MusicKeys : MonoBehaviour
{
    private void Start()
    {
        if (TryGetComponent(out Button button)) {
            button.onClick.AddListener(() => Managers.Sound.Play("piano" + this.name)); // TODO: 소리 너무 작음
        }
    }
}