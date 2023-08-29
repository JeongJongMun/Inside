using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KillerRoomHangerInput : Trick
{
    [Header("HangerHole �����ΰ� (���� ���°�)")]
    public GameObject hangerHoleMoved;

    [Header("������ ���� ĳ���")]
    public GameObject cabinetKillerOpen;

    [Header("ĳ��� �̹��� ����")]
    public Sprite cabinetKillerFinal;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            Debug.LogFormat("{0} Solved", this.name);
            GetComponent<Button>().interactable = false;
            SoundManager.instance.SFXPlay("pulloverCloset");
            StartCoroutine(ForPlaySFX());
        }
    }

    private IEnumerator ForPlaySFX()
    {
        SetIsSolved(true);
        yield return new WaitForSeconds(2.0f);
        SoundManager.instance.SFXPlay("doorSlide");
        SolvedAction();
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
