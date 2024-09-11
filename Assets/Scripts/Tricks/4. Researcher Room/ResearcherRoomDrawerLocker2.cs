using UnityEngine;
using UnityEngine.UI;

public class ResearcherRoomDrawerLocker2 : Trick
{
    public Image drawer;

    public GameObject toggleGroup;

    public GameObject goldKey;

    public GameObject drawerLock2Zoom;
    public GameObject drawerLocker2HolderZoom;
    public GameObject locker2;

    public GameObject drawerLock2ZoomOpened;
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
        gameObject.SetActive(false);
    }
}
