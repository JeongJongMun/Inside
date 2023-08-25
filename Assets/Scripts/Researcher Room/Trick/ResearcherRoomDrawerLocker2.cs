using UnityEngine;
using UnityEngine.UI;

public class ResearcherRoomDrawerLocker2 : Trick
{
    [Header("Drawer 이미지")]
    public Image drawer;

    [Header("자물쇠 버튼 그룹")]
    public GameObject toggleGroup;

    [Header("황금 키")]
    public GameObject goldKey;

    [Header("## 자물쇠 2번 해제 시 OFF 객체 ##")]
    [Header("자물쇠2 확대 잠김 이미지")]
    public GameObject drawerLock2Zoom;
    [Header("자물쇠2 걸쇠 확대")]
    public GameObject drawerLocker2HolderZoom;
    [Header("자물쇠2")]
    public GameObject locker2;

    [Header("## 자물쇠 2번 해제 시 ON 객체 ##")]
    [Header("자물쇠2 확대 열린 이미지")]
    public GameObject drawerLock2ZoomOpened;
    [Header("서랍장 자물쇠2 풀린 이미지")]
    public Sprite drawer2Opened;


    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "DrawerLocker2")
        {
            ToggleGroupLimit toggleGroupLimit = toggleGroup.GetComponent<ToggleGroupLimit>();
            if (toggleGroupLimit.selectedNumbers.Contains(9) && toggleGroupLimit.selectedNumbers.Contains(3) && toggleGroupLimit.selectedNumbers.Contains(0))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("lockerSuccess");
                drawer.sprite = drawer2Opened;
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                foreach (Toggle t in toggleGroupLimit.toggles)
                {
                    t.isOn = false;
                }
            }
        }
    }
    public override void SolvedAction()
    {
        drawerLock2Zoom.SetActive(false);
        drawerLocker2HolderZoom.SetActive(false);
        locker2.SetActive(false);
        drawerLock2ZoomOpened.SetActive(true);
        goldKey.SetActive(true);
        drawer.sprite = drawer2Opened;
        gameObject.SetActive(false);
    }
}
