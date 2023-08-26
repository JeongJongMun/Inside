using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HallwayManager : MonoBehaviour
{
    public void OnClickDoor()
    {
        SoundManager.instance.SFXPlay("doorOpen");
        StartCoroutine(LoadIdolRoom());
    }
    public void OnClickStair()
    {
        SoundManager.instance.SFXPlay("stair");
        StartCoroutine(LoadLivingRoom());
    }

    private IEnumerator LoadIdolRoom()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("IdolRoom");
    }

    private IEnumerator LoadLivingRoom()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("LivingRoom");
    }
}
