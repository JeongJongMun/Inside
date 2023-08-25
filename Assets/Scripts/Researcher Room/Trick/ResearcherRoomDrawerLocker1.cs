using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResearcherRoomDrawerLocker1 : Trick
{

    [Header("정답")]
    public List<string> answers = new List<string>()
    { "Left", "Right", "Right", "Down", "Down", "Right", "Left", "Left", "Left", "Down"};

    [Header("입력 방향")]
    public List<string> inputs = new List<string>();


    [Header("DrawerZoom 이미지")]
    public Image drawerZoom;

    [Header("DrawerZoom 이미지가 바뀔 Drawer1Open 이미지")]
    public Sprite drawer1Open;

    [Header("Drawer Locker 1 Zoom 이미지")]
    public Image drawerLocker1Zoom;

    [Header("Drawer Locker 1 Zoom 이미지가 바뀔 열린 이미지")]
    public Sprite drawerLocker1ZoomOpen;

    [Header("자물쇠1 트릭 성공 시 On")]
    public GameObject coins;
    public GameObject latch3;

    [Header("자물쇠1 트릭 성공 시 Off")]
    public GameObject drawerZoomLocker1;
    public GameObject drawerLockerHolder1Zoom;


    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Enumerable.SequenceEqual(inputs, answers))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("lockerSuccess");
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
            }
        }
    }
    public override void SolvedAction()
    {
        drawerZoom.sprite = drawer1Open;
        drawerLocker1Zoom.sprite = drawerLocker1ZoomOpen;
        drawerZoomLocker1.SetActive(false);
        drawerLockerHolder1Zoom.SetActive(false);
        coins.SetActive(true);
        latch3.SetActive(true);
        gameObject.SetActive(false);
    }
}
