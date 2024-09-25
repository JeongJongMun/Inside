using UnityEngine;

public class BaseScene : MonoBehaviour
{
    protected virtual void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public virtual void Init()
    {
        Managers.UI.CloseLoadingUI();
    }
        
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // 앱이 백그라운드로 전환될 때 실행될 코드
            Debug.Log("앱이 백그라운드로 전환됨");
        }
        else
        {
            // 앱이 다시 활성화될 때 실행될 코드
            Debug.Log("앱이 다시 활성화됨");
        }
    }
}