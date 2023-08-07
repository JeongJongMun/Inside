using System;
using UnityEngine;
using UnityEngine.UI;

public class KidRoomSwitch : Trick
{
    [Header("½ºÀ§Ä¡ ÀÌ¹ÌÁö ([0] = On, [1] = Off)")]
    public Sprite[] switchSprite;

    [Header("ºÒ²¨Áü È¿°ú ÆÐ³Î")]
    public GameObject lightPanel;

    [Header("Çü±¤ ±Û¾¾")]
    public GameObject fluorescentTime;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == "Switch")
        {
            if (!IsSolved())
            {
                Debug.Log("Switch Off");
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.Log("Switch On");
                SetIsSolved(false);
                SolvedAction();
            }
        }
    }
    public override void SolvedAction()
    {
        this.GetComponent<Image>().sprite = switchSprite[Convert.ToInt32(!IsSolved())];
        lightPanel.SetActive(IsSolved());
        fluorescentTime.SetActive(IsSolved());
    }
}
