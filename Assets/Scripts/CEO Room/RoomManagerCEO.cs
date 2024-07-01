using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomManagerCEO : RoomManager
{
    public GameObject drawerCEO;
    public GameObject safeCEO;
    public Button[] drawerPWButtons;
    public Button[] safePWButtons;

    public override void Start()
    {
        base.Start();
        foreach (Button btn in drawerPWButtons)
        {
            btn.onClick.AddListener(() => OnClickPasswordButton(drawerCEO, btn.transform.GetChild(0).GetComponent<TMP_Text>()));
        }
        foreach (Button btn in safePWButtons)
        {
            btn.onClick.AddListener(() => OnClickPasswordButton(safeCEO, btn.transform.GetChild(0).GetComponent<TMP_Text>()));
        }
    }
    public void OnClickDoor()
    {
        SoundManager.instance.SFXPlay("doorOpen");
        StartCoroutine(LoadLivingRoom());
    }
    public void OnClickDrawer(GameObject drawerOpen)
    {
        drawerOpen.SetActive(!drawerOpen.activeSelf);
        SoundManager.instance.SFXPlay("drawerOpened");
    }

    public void OnClickPasswordButton(GameObject trickObject, TMP_Text password)
    {
        if (trickObject.GetComponent<Trick>().IsSolved()) return;
        
        password.text = ((int.Parse(password.text) + 1) % 10).ToString();
        SoundManager.instance.SFXPlay("electricButton");
        OnClickTrick(trickObject);
    }

    public void OnClickDrawer1(GameObject drawerOpen)
    {
        if (drawerCEO.GetComponent<CEORoomDrawerCEO>().IsSolved())
        {
            OnClickDrawer(drawerOpen);
            SoundManager.instance.SFXPlay("drawerOpened");
        }
    }

    // for Play Door Open SFX
    private IEnumerator LoadLivingRoom()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("LivingRoom");
    }

}
