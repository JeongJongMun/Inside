using UnityEngine;

public class MusicBoxOpened : MonoBehaviour
{
    private void OnEnable()
    {
        // ������ Ŭ�� ��
        SoundManager.instance.OnMusicbox();
    }

    private void OnDisable()
    {
        // ������ ������
        SoundManager.instance.OutMusicbox();
    }
}
