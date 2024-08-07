using UnityEngine.SceneManagement;
/* 상태 목록
 OutGame
 |-- Login
 |-- SignUp
 |-- Main
 InGame
 |-- Kid
 |-- Idol
 |-- Hallway
 |-- Living
 |-- Researcher
 |-- CEO
 |-- Killer
 |-- Ending
 |-- Credit
 */
public interface IState
{
    void Enter();
    void Exit();
}
public class OutGameState : IState
{
    public void Enter()
    {
        // TODO: 메인 브금 재생
        if (SceneManager.GetActiveScene().name != "0. OutGame") {
            SceneManager.LoadScene("0. OutGame");
        }
    }
    public void Exit()
    {
    }
}
public class InGameState : IState
{
    public void Enter()
    {
        if (SceneManager.GetActiveScene().name != "1. InGame") {
            SceneManager.LoadScene("1. InGame");
        }
        // TODO: 인벤토리 초기화
        // Inventory.instance.ClearInventory();
    }
    public void Exit()
    {
    }
}
#region OutGameStates
public class LoginState : IState
{
    public void Enter()
    {
        AuthUI.instance.loginPanel.SetActive(true);
        AuthUI.instance.ClearInputFields();
    }
    public void Exit()
    {
        AuthUI.instance.loginPanel.SetActive(false);
    }
}
public class SignUpState : IState
{
    public void Enter()
    {
        AuthUI.instance.signUpPanel.SetActive(true);
        AuthUI.instance.ClearInputFields();
    }
    public void Exit()
    {
        AuthUI.instance.signUpPanel.SetActive(false);
    }
}
public class MainState : IState
{
    public void Enter()
    {
        MainUI.instance.mainPanel.SetActive(true);
    }
    public void Exit()
    {
        MainUI.instance.mainPanel.SetActive(false);
    }
}
#endregion

#region InGameStates
public class KidState : IState
{
    public void Enter()
    {
    }
    public void Exit()
    {
    }
}
public class IdolState : IState
{
    public void Enter()
    {
    }
    public void Exit()
    {
    }
}
public class HallwayState : IState
{
    public void Enter()
    {
    }
    public void Exit()
    {
    }
}
public class LivingState : IState
{
    public void Enter()
    {
    }
    public void Exit()
    {
    }
}
public class ResearcherState : IState
{
    public void Enter()
    {
    }
    public void Exit()
    {
    }
}
public class CEOState : IState
{
    public void Enter()
    {
    }
    public void Exit()
    {
    }
}
public class KillerState : IState
{
    public void Enter()
    {
    }
    public void Exit()
    {
    }
}
public class EndingState : IState
{
    public void Enter()
    {
    }
    public void Exit()
    {
    }
}
public class CreditState : IState
{
    public void Enter()
    {
    }
    public void Exit()
    {
    }
}
#endregion