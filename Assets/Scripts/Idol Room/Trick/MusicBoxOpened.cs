using UnityEngine;

public class MusicBoxOpened : MonoBehaviour
{
    private void OnEnable()
    {
        // 오르골 클릭 시
        SoundManager.instance.OnMusicbox();
    }

    private void OnDisable()
    {
        // 오르골 나갈때
        SoundManager.instance.OutMusicbox();
    }
}
