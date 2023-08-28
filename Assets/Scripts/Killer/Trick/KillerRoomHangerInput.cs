using UnityEngine;
using UnityEngine.UI;

public class KillerRoomHangerInput : Trick
{
    [Header("HangerHole 움직인거 (구멍 없는거)")]
    public GameObject hangerHoleMoved;

    [Header("움직일 열린 캐비넷")]
    public GameObject cabinetKillerOpen;

    [Header("캐비넷 이미지 변경")]
    public Sprite cabinetKillerFinal;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            Debug.LogFormat("{0} Solved", this.name);
            SoundManager.instance.SFXPlay("pulloverCloset");
            SetIsSolved(true);
            SolvedAction();
            SoundManager.instance.SFXPlay("doorSlide");
        }
    }


    public override void SolvedAction()
    {
        hangerHoleMoved.SetActive(true);
        GetComponent<RectTransform>().anchoredPosition += Vector2.right * 200;
        GetComponent<Image>().raycastTarget = false;
        cabinetKillerOpen.GetComponent<RectTransform>().anchoredPosition += Vector2.right * 412;
        cabinetKillerOpen.GetComponent<Image>().sprite = cabinetKillerFinal;
    }
}
