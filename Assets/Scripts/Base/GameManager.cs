using UnityEngine;
using UnityEngine.SceneManagement;
/* GameManager.cs
 * 게임 상태를 관리하는 스크립트
 * 전략 패턴을 사용하여 상태에 따라 UI를 변경
 */
public class GameManager : MonoBehaviour
{
#region Private Variables
    private static GameManager instance = null;
    private IState externalState = null;
    private IState internalState = null;
    private const string OutGameScene = "0. OutGame";
    private const string InGameScene = "1. InGame";
#endregion

#region Public Variables
    public static GameManager Instance { get { return instance; } }
    public SoundManager soundManager = new SoundManager();
#endregion

#region Private Methods
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.AddComponent<AuthManager>();
            gameObject.AddComponent<DatabaseManager>();
        }
    }
    private void Start()
    {
        soundManager.Init();
        if (SceneManager.GetActiveScene().name == OutGameScene) {
            ChangeState(new OutGameState(), true);
        }
        else if (SceneManager.GetActiveScene().name == InGameScene) {
            ChangeState(new InGameState(), true);
        }
    }
#endregion

#region Public Methods
    public void ChangeState(IState newState, bool isExternalState = false)
    {
        if (isExternalState) {
            if (externalState != null) {
                externalState.Exit();
            }
            externalState = newState;
            externalState.Enter();

            switch (externalState) {
                case OutGameState:
                    ChangeState(new LoginState());
                    break;
                case InGameState:
                    ChangeState(new KidState());
                    break;
            }
            return;
        }

        if (internalState != null) {
            internalState.Exit();
        }
        internalState = newState;
        internalState.Enter();
        Debug.Log($"Current State - External: {externalState.GetType().Name}, Internal: {internalState.GetType().Name}");
    }
#endregion
}