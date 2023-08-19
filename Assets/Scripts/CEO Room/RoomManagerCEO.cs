using TMPro;
using UnityEngine;
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
        SceneManager.LoadScene("LivingRoom");
    }
    public void OnClickDrawer(GameObject drawerOpen)
    {
        drawerOpen.SetActive(!drawerOpen.activeSelf);
    }

    // 서랍 && 금고 비밀번호 클릭 시 호출
    public void OnClickPasswordButton(GameObject trickObject, TMP_Text password)
    {
        if (trickObject.GetComponent<Trick>().IsSolved()) return;
        
        password.text = ((int.Parse(password.text) + 1) % 10).ToString();
        OnClickTrick(trickObject);
    }

    public void OnClickDrawer1(GameObject drawerOpen)
    {
        if (drawerCEO.GetComponent<CEORoomDrawerCEO>().IsSolved())
        {
            OnClickDrawer(drawerOpen);
        }
    }


}
