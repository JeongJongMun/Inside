using UnityEngine;
using UnityEngine.UI;

public class ResearcherRoomClothes : Trick
{
    [Header("옷 이동한 이미지")]
    public Sprite clothesMove;

    [Header("힌트 오브젝트")]
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
