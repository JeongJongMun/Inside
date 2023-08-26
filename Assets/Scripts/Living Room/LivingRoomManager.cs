using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using TMPro;
using static Define;

public class LivingRoomManager : RoomManager
{
    public void OnClickHallwayStair()
    {
        SoundManager.instance.SFXPlay("stair");
        StartCoroutine(LoadHallway());
    }
    public void OnClickResearcherDoor()
    {
        if (DatabaseManager.Instance.GetData(TrickName.CardReader))
        {
            SoundManager.instance.SFXPlay("doorOpen");
            StartCoroutine(LoadResearcherRoom());
        }
        else
            SoundManager.instance.SFXPlay("doorLocked");
    }
    public void OnClickCEODoor()
    {
        if (DatabaseManager.Instance.GetData(TrickName.CoinMachine))
        {
            SoundManager.instance.SFXPlay("doorOpen");
            StartCoroutine(LoadCEORoom());
        }
        else
            SoundManager.instance.SFXPlay("doorLocked");
    }
    public void OnClickOutDoor()
    {
        SoundManager.instance.SFXPlay("doorLocked");
    }
    public void OnClickHatch()
    {
        SoundManager.instance.SFXPlay("doorOpen");
        StartCoroutine(LoadKillerRoom());
    }
    public void OnClick500Button(TMP_Text money)
    {
        if (Inventory.Instance.IsClicked(ItemName.Coins))
        {
            SoundManager.instance.SFXPlay("insertCoin");
            money.text = (int.Parse(money.text) + 500).ToString();
        }
    }
    public void OnClick100Button(TMP_Text money)
    {
        if (Inventory.Instance.IsClicked(ItemName.Coins))
        {
            SoundManager.instance.SFXPlay("insertCoin");
            money.text = (int.Parse(money.text) + 100).ToString();
        }
    }
    public void OnClickReturnButton(TMP_Text money)
    {
        SoundManager.instance.SFXPlay("electricButton");
        money.text = "0";
    }

    // For Play DoorOpen SFX
    private IEnumerator LoadHallway()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Hallway");
    }
    private IEnumerator LoadResearcherRoom()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("ResearcherRoom");
    }
    private IEnumerator LoadCEORoom()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("CEORoom");
    }
    private IEnumerator LoadKillerRoom()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("KillerRoom");
    }
}
