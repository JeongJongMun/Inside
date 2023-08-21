using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using static Define;

public class LivingRoomManager : RoomManager
{
    public void OnClickHallwayStair()
    {
        SceneManager.LoadScene("Hallway");
    }
    public void OnClickResearcherDoor()
    {
        if (DatabaseManager.Instance.IsTrickSolved(Define.RoomName.Living, Define.TrickName.CardReader))
            SceneManager.LoadScene("ResearcherRoom");
        else
            Debug.Log("문 잠김 소리 재생");
    }
    public void OnClickCEODoor()
    {
        if (DatabaseManager.Instance.IsTrickSolved(Define.RoomName.Living, Define.TrickName.CoinMachine))
            SceneManager.LoadScene("CEORoom");
        else
            Debug.Log("문 잠김 소리 재생");
    }
    public void OnClickOutDoor()
    {
        Debug.Log("문 잠김 소리 재생");
    }
    public void OnClick500Button(TMP_Text money)
    {
        if (Inventory.Instance.IsClicked(ItemName.Coins))
            money.text = (int.Parse(money.text) + 500).ToString();
    }
    public void OnClick100Button(TMP_Text money)
    {
        if (Inventory.Instance.IsClicked(ItemName.Coins))
            money.text = (int.Parse(money.text) + 100).ToString();
    }
    public void OnClickReturnButton(TMP_Text money)
    {
        money.text = "0";
    }
}
