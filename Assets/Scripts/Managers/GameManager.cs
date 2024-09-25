// ----- Unity
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    // ----- Private
    private IState externalState = null;
    private IState internalState = null;
    private const string OutGameScene = "0. OutGame";
    private const string InGameScene = "1. InGame";
    
    // --------------------------------------------------
    // Functions - Event
    // --------------------------------------------------
    protected override void Awake()
    {
        base.Awake();
        gameObject.AddComponent<DatabaseManager>();
    }
    private void Start()
    {
        Managers.Sound.Init();
        if (SceneManager.GetActiveScene().name == OutGameScene) {
            ChangeState(new OutGameState(), true);
        }
        else if (SceneManager.GetActiveScene().name == InGameScene) {
            ChangeState(new InGameState(), true);
        }
    }

    // --------------------------------------------------
    // Functions - Normal
    // --------------------------------------------------
    public void ChangeState(IState newState, bool isExternalState = false)
    {
        if (isExternalState) 
        {
            if (externalState != null) 
                externalState.Exit();
            
            externalState = newState;
            externalState.Enter();

            switch (externalState) 
            {
                case OutGameState:
                    ChangeState(new LoginState());
                    break;
                case InGameState:
                    ChangeState(new KidState());
                    break;
            }
            return;
        }

        if (internalState != null) 
            internalState.Exit();
        
        internalState = newState;
        internalState.Enter();
        Debug.Log($"Current State - External: {externalState.GetType().Name}, Internal: {internalState.GetType().Name}");
    }
}