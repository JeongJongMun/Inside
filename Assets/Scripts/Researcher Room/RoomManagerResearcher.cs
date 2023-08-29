using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoomManagerResearcher : RoomManager
{
    public void OnClickDoor()
    {
        SoundManager.instance.SFXPlay("doorOpen");
        StartCoroutine(ForPlaySFX());
    }
    public void OnClickFlower(GameObject panel)
    {
        if (Inventory.Instance.IsClicked(Define.ItemName.Magnifier))
        {
            ZoomIn(panel);
        }
    }
    public void OnclickBook()
    {
        SoundManager.instance.SFXPlay("turnThePage");
    }
    private IEnumerator ForPlaySFX()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("LivingRoom");
    }
}
