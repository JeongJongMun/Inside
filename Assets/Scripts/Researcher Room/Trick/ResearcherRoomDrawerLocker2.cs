using UnityEngine;
using UnityEngine.UI;

public class ResearcherRoomDrawerLocker2 : Trick
{
    [Header("Drawer �̹���")]
    public Image drawer;

    [Header("�ڹ��� ��ư �׷�")]
    public GameObject toggleGroup;

    [Header("Ȳ�� Ű")]
    public GameObject goldKey;

    [Header("## �ڹ��� 2�� ���� �� OFF ��ü ##")]
    [Header("�ڹ���2 Ȯ�� ��� �̹���")]
    public GameObject drawerLock2Zoom;
    [Header("�ڹ���2 �ɼ� Ȯ��")]
    public GameObject drawerLocker2HolderZoom;
    [Header("�ڹ���2")]
    public GameObject locker2;

    [Header("## �ڹ��� 2�� ���� �� ON ��ü ##")]
    [Header("�ڹ���2 Ȯ�� ���� �̹���")]
    public GameObject drawerLock2ZoomOpened;
    [Header("������ �ڹ���2 Ǯ�� �̹���")]
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
