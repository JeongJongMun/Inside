using UnityEngine;
using UnityEngine.UI;

public class ResearcherRoomClothes : Trick
{
    [Header("�� �̵��� �̹���")]
    public Sprite clothesMove;

    [Header("��Ʈ ������Ʈ")]
    public GameObject hint;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name && !IsSolved())
        {
            Debug.LogFormat("{0} Solved", this.name);
            SoundManager.instance.SFXPlay("closetSlide");
            SetIsSolved(true);
            SolvedAction();
        }
    }
    public override void SolvedAction()
    {
        GetComponent<Image>().sprite = clothesMove;
        hint.SetActive(true);
        GetComponent<Button>().interactable = false;
    }
}
